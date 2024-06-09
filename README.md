# pdf2img

pdf2img is a .NET application that converts PDF pages into image files. It supports multiple output formats, DPI settings, and rendering flags for customized image generation.

## Features

- Convert PDF pages to various image formats (PNG, JPEG).
- Specify DPI for high-quality image rendering.
- Support for rendering flags (e.g., Annotations, LcdText).
- Convert specific page ranges.
- Option to overwrite existing files.
- Verbose mode for detailed output.

## Usage

pdf2img.exe <input.pdf> <output directory> [options]

markdown


### Options

- `--dpi <dpi>`: Resolution in DPI for the output image files. Use 72 for original PDF size. Default is 72.
- `--pagerange <range>`: Range of pages to convert (e.g., 1-3,5).
- `--format <format>`: Output image format (png, jpeg). Default is png.
- `--quality <quality>`: Image quality for JPEG format (1-100). Default is 90.
- `--overwrite`: Overwrite existing files in the output directory.
- `--verbose`: Enable verbose output.
- `--renderflags <flags>`: Rendering flags (comma-separated values). Options:
  - `None`: 0
  - `ForPrinting`: 0x800
  - `Annotations`: 1
  - `LcdText`: 2
  - `NoNativeText`: 4
  - `Grayscale`: 8
  - `LimitImageCacheSize`: 0x200
  - `ForceHalftone`: 0x400
  - `Transparent`: 0x1000
  - `CorrectFromDpi`: 0x2000

### Examples

Convert all pages of a PDF to PNG images at 300 DPI:

    ```
    pdf2img.exe "C:\Path\To\Input.pdf" "C:\Path\To\OutputDirectory" --dpi 300
    ```

Convert specific pages (1-3 and 5) of a PDF to JPEG images with high quality:

    ```
    pdf2img.exe "C:\Path\To\Input.pdf" "C:\Path\To\OutputDirectory" --format jpeg --quality 100 --pagerange 1-3,5
    ```

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.