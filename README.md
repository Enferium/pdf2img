# PDFImageConverter

PDFImageConverter is a .NET application that converts PDF pages into image files. It supports multiple output formats, DPI settings, and rendering flags for customized image generation.

## Features

- Convert PDF pages to various image formats (PNG, JPEG).
- Specify DPI for high-quality image rendering.
- Support for rendering flags (e.g., Annotations, LcdText).
- Convert specific page ranges.
- Option to overwrite existing files.
- Verbose mode for detailed output.

## Requirements

- .NET 6.0 SDK or later
- PdfiumViewer library
- CommandLineParser library
- System.Drawing.Common library

## Installation

1. **Clone the repository**:

    ```
    git clone https://github.com/yourusername/PDFImageConverter.git
    cd PDFImageConverter
    ```

2. **Install dependencies**:

    Use the NuGet Package Manager to install the necessary dependencies:

    ```
    dotnet add package CommandLineParser --version 2.9.1
    dotnet add package PdfiumViewer --version 2.13.0
    dotnet add package PdfiumViewer.Native.x86_64.v8-xfa --version 2018.4.8.256
    dotnet add package System.Drawing.Common --version 8.0.6
    ```

## Building and Publishing

1. **Build the project**:

    Open the project in Visual Studio and build it, or use the .NET CLI:

    ```
    dotnet build
    ```

2. **Publish the project**:

    To publish the project as a single executable:

    ```
    dotnet publish -c Release -r win-x64 --self-contained
    ```

    This will create a single executable file in the `bin\Release\net6.0\win-x64\publish` directory.

## Usage

PDFImageConverter.exe <input.pdf> <output directory> [options]

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

PDFImageConverter.exe "C:\Path\To\Input.pdf" "C:\Path\To\OutputDirectory" --dpi 300

sql


Convert specific pages (1-3 and 5) of a PDF to JPEG images with high quality:

PDFImageConverter.exe "C:\Path\To\Input.pdf" "C:\Path\To\OutputDirectory" --format jpeg --quality 100 --pagerange 1-3,5

css


## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.