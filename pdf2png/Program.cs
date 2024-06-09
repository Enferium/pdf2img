using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using CommandLine;
using PdfiumViewer;

namespace PDFImageConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            Parser.Default.ParseArguments<Options>(args)
                .WithParsed<Options>(opts => RunOptionsAndReturnExitCode(opts))
                .WithNotParsed<Options>((errs) => HandleParseError(errs));
        }

        static void RunOptionsAndReturnExitCode(Options opts)
        {
            if (!File.Exists(opts.InputFile))
            {
                Console.WriteLine("The specified PDF file does not exist.");
                return;
            }

            if (!Directory.Exists(opts.OutputDirectory))
            {
                Directory.CreateDirectory(opts.OutputDirectory);
            }

            try
            {
                using (var document = PdfDocument.Load(opts.InputFile))
                {
                    var pagesToConvert = GetPageRange(opts.PageRange, document.PageCount);
                    var renderFlags = ParseRenderFlags(opts.RenderFlags);

                    foreach (var pageIndex in pagesToConvert)
                    {
                        using (var image = RenderPage(document, pageIndex, opts.Dpi, renderFlags))
                        {
                            string outputPath = Path.Combine(opts.OutputDirectory, $"Page_{pageIndex + 1}.{opts.Format}");

                            if (File.Exists(outputPath) && !opts.Overwrite)
                            {
                                Console.WriteLine($"File {outputPath} already exists. Use --overwrite to overwrite existing files.");
                                continue;
                            }

                            SaveImage(image, outputPath, opts.Format, opts.Quality);
                            if (opts.Verbose)
                            {
                                Console.WriteLine($"Saved: {outputPath}");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }

        static IEnumerable<int> GetPageRange(string range, int pageCount)
        {
            var pages = new List<int>();

            if (string.IsNullOrEmpty(range))
            {
                for (int i = 0; i < pageCount; i++)
                {
                    pages.Add(i);
                }
            }
            else
            {
                foreach (var part in range.Split(','))
                {
                    if (part.Contains('-'))
                    {
                        var bounds = part.Split('-');
                        int start = int.Parse(bounds[0]) - 1;
                        int end = int.Parse(bounds[1]) - 1;
                        for (int i = start; i <= end; i++)
                        {
                            pages.Add(i);
                        }
                    }
                    else
                    {
                        pages.Add(int.Parse(part) - 1);
                    }
                }
            }

            return pages;
        }

        static Bitmap RenderPage(PdfDocument document, int pageIndex, int dpi, PdfRenderFlags flags)
        {
            var page = document.Render(pageIndex, dpi, dpi, flags);
            return new Bitmap(page);
        }

        static void SaveImage(Bitmap image, string outputPath, string format, int quality)
        {
            if (format.Equals("jpeg", StringComparison.OrdinalIgnoreCase))
            {
                var encoder = GetEncoder(ImageFormat.Jpeg);
                var encoderParams = new EncoderParameters(1);
                encoderParams.Param[0] = new EncoderParameter(Encoder.Quality, quality);
                image.Save(outputPath, encoder, encoderParams);
            }
            else
            {
                image.Save(outputPath, ImageFormat.Png);
            }
        }

        static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }

        static PdfRenderFlags ParseRenderFlags(string flags)
        {
            PdfRenderFlags renderFlags = PdfRenderFlags.None;
            if (string.IsNullOrEmpty(flags)) return renderFlags;

            var flagList = flags.Split(',');
            foreach (var flag in flagList)
            {
                if (Enum.TryParse(flag, true, out PdfRenderFlags parsedFlag))
                {
                    renderFlags |= parsedFlag;
                }
                else
                {
                    Console.WriteLine($"Invalid render flag: {flag}");
                }
            }

            return renderFlags;
        }

        static void HandleParseError(IEnumerable<Error> errs)
        {
            // Handle errors here
            Console.WriteLine("Error parsing arguments.");
        }
    }
}
