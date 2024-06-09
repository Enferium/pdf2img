using CommandLine;

namespace PDFImageConverter
{
    public class Options
    {
        [Value(0, MetaName = "input", HelpText = "Input PDF file path.", Required = true)]
        public string InputFile { get; set; }

        [Value(1, MetaName = "output", HelpText = "Output directory path.", Required = true)]
        public string OutputDirectory { get; set; }

        [Option("dpi", Default = 72, HelpText = "Resolution in DPI for the output image files. Use 72 for original PDF size.")]
        public int Dpi { get; set; }

        [Option("pagerange", HelpText = "Range of pages to convert (e.g., 1-3,5).")]
        public string PageRange { get; set; }

        [Option("format", Default = "png", HelpText = "Output image format (png, jpeg).")]
        public string Format { get; set; }

        [Option("quality", Default = 90, HelpText = "Image quality for JPEG format (1-100).")]
        public int Quality { get; set; }

        [Option("overwrite", Default = false, HelpText = "Overwrite existing files in the output directory.")]
        public bool Overwrite { get; set; }

        [Option("verbose", Default = false, HelpText = "Enable verbose output.")]
        public bool Verbose { get; set; }

        [Option("renderflags", Default = "Annotations,CorrectFromDpi,LcdText", HelpText = "Rendering flags (comma separated values). Options:\n" +
            "None = 0,\n" +
            "ForPrinting = 0x800, Render for printing.\n" +
            "Annotations = 1, Set if annotations are to be rendered.\n" +
            "LcdText = 2, Set if using text rendering optimized for LCD display.\n" +
            "NoNativeText = 4, Don't use the native text output available on some platforms.\n" +
            "Grayscale = 8, Grayscale output.\n" +
            "LimitImageCacheSize = 0x200, Limit image cache size.\n" +
            "ForceHalftone = 0x400, Always use halftone for image stretching.\n" +
            "Transparent = 0x1000, Render with a transparent background.\n" +
            "CorrectFromDpi = 0x2000, Correct height/width for DPI."
        )]
        public string RenderFlags { get; set; }
    }
}
