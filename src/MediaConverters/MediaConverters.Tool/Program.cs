// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;

ImageDecoder d = WebpDecoder.Instance;

var file = @"E:\Download\file_example_TIFF_1MB.tiff";
var buffer = File.ReadAllBytes(file);
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
