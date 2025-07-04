// See https://aka.ms/new-console-template for more information

using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Bmp;
using SixLabors.ImageSharp.Formats.Tiff;
using SixLabors.ImageSharp.Formats.Webp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

ImageDecoder d = WebpDecoder.Instance;

var tiffFile = @"E:\Download\file_example_TIFF_1MB.tiff";
var file = @"E:\Download\file_example_JPG_100kB.jpg";
var buffer = File.ReadAllBytes(file);

var image = Image.Load<Rgba32>(buffer);
image.Mutate(context => context.Resize(new Size(100, 100), compand: true));
Console.WriteLine(image.Width);
image.SaveAsPng("1.png");

Console.WriteLine("Hello, World!");
