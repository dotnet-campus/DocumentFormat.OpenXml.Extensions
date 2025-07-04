// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

using System.Text;

ImageDecoder d = WebpDecoder.Instance;

var maxPixelCount = 100_00_00;

var w = 1500;
var h = 1300;

var s = w / (double) h;

var pw = (int) Math.Sqrt(maxPixelCount * w / (double) h);
var ph = (int) Math.Sqrt(maxPixelCount * h / (double) w);

var s1 = pw / (double) ph;



var tiffFile = @"E:\Download\file_example_TIFF_1MB.tiff";
var file = @"E:\Download\file_example_favicon.ico";
var buffer = File.ReadAllBytes(file);

try
{
    var image = Image.Load<Rgba32>(buffer);
    image.Mutate(context => context.Resize(new Size(100, 100), compand: true));
    Console.WriteLine(image.Width);
    image.SaveAsPng("1.png");

    
}
catch (ImageFormatException e)
{
}

Console.WriteLine("Hello, World!");
