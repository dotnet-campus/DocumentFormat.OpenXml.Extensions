// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;

ImageDecoder d = WebpDecoder.Instance;

var tiffFile = @"E:\Download\file_example_TIFF_1MB.tiff";
var file = @"E:\Download\file_example_favicon.ico";
var buffer = File.ReadAllBytes(file);

var tiffImageFormatDetector = new TiffImageFormatDetector();
if (tiffImageFormatDetector.TryDetectFormat(File.ReadAllBytes(tiffFile),out var f))
{
    
}

var detector = new BmpImageFormatDetector();
if (detector.TryDetectFormat(buffer,out var format))
{
    
}

var detectFormat = Image.DetectFormat(buffer);
// ImageFormatManager.ThrowInvalidDecoder(configuration.ImageFormatsManager);
var imageInfo = Image.Identify(buffer);
Image image = Image.Load<Rgba32>(buffer);

foreach (IImageFormat imageFormat in Configuration.Default.ImageFormats)
{
    Console.WriteLine(imageFormat.Name);
}

/*
   PNG
   JPEG
   GIF
   BMP
   PBM
   TGA
   TIFF
   Webp
   QOI
 */
Console.WriteLine("Hello, World!");
