// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Webp;

ImageDecoder d = WebpDecoder.Instance;

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
