using System.Xml.Linq;
using SkiaSharp;
using Svg.Skia;

namespace DotNetCampus.MediaConverter.SkiaWmfRenderer.Optimizations;

/// <summary>
/// 提供 SVG 文件相关的优化与修复方法，例如将 SVG 转换为 PNG，或修复 SVG 中的无效字符。
/// </summary>
public static class SvgFileOptimization
{
    /// <summary>
    /// 将指定上下文中的 SVG 文件渲染并保存为 PNG 文件。
    /// </summary>
    /// <param name="context">包含 SVG 文件和工作目录等信息的上下文。</param>
    /// <returns>成功时返回生成的 PNG 文件信息；失败时返回 null。</returns>
    public static FileInfo? ConvertSvgToPngFile(in EnhancedGraphicsMetafileOptimizationContext context)
    {
        var imageFile = context.ImageFile;
        var workingFolder = context.WorkingFolder;

        using var skSvg = new SKSvg();
        using var skPicture = skSvg.Load(imageFile.FullName);
        var outputFile = Path.Join(workingFolder.FullName,
            $"SVG_{Path.GetRandomFileName()}.png");
        var canSave = skSvg.Save(outputFile, SKColors.Transparent);
        if (canSave && File.Exists(outputFile))
        {
            return new FileInfo(outputFile);
        }

        // 转换失败
        return null;
    }

    /// <summary>
    /// 异步修复 SVG 文件中可能包含的无效字符（例如替换或删除不可见占位符），并在需要时将修复后的文件写入工作目录。
    /// </summary>
    /// <param name="svgFile">要修复的 SVG 文件。</param>
    /// <param name="workingFolder">修复后文件写入的工作目录。</param>
    /// <returns>修复后文件的 <see cref="FileInfo"/>；如果未作修改则返回原始文件实例。</returns>
    public static async Task<FileInfo> FixSvgInvalidCharacterAsync(FileInfo svgFile,
        DirectoryInfo workingFolder)
    {
        using var fileStream = svgFile.OpenRead();
        using var streamReader = new StreamReader(fileStream);

        var xDocument = await XDocument.LoadAsync(streamReader, LoadOptions.SetLineInfo, CancellationToken.None);
        bool anyUpdate = false;

        foreach (var xElement in xDocument.Descendants("text"))
        {
            var value = xElement.Value;
            if (!string.IsNullOrEmpty(value) && value.Length > 0 && value[0] is var c && c == 0xFFFD)
            {
                // 0xFFFFD 是 utf8 特殊字符
                // 画出来就是�符号，不如删掉
                xElement.Value = string.Empty;

                anyUpdate = true;
            }
        }

        if (anyUpdate)
        {
            var convertedFile = Path.Join(workingFolder.FullName, $"FixSVG_{Path.GetRandomFileName()}.svg");
            using var stream = File.Create(convertedFile);
            await xDocument.SaveAsync(stream, SaveOptions.None, CancellationToken.None);
            return new FileInfo(convertedFile);
        }

        // 啥都不用改，返回原图
        return svgFile;
    }

    /// <summary>
    /// 同步修复 SVG 文件中可能包含的无效字符（例如替换或删除不可见占位符），并在需要时将修复后的文件写入工作目录。
    /// </summary>
    /// <param name="context">包含 SVG 文件和工作目录等信息的上下文。</param>
    /// <returns>修复后文件的 <see cref="FileInfo"/>；如果未作修改则返回原始文件实例。</returns>
    public static FileInfo FixSvgInvalidCharacter(EnhancedGraphicsMetafileOptimizationContext context)
    {
        FileInfo svgFile = context.ImageFile;
        DirectoryInfo workingFolder = context.WorkingFolder;

        using var fileStream = svgFile.OpenRead();
        using var streamReader = new StreamReader(fileStream);

        var xDocument = XDocument.Load(streamReader, LoadOptions.SetLineInfo);
        bool anyUpdate = false;

        foreach (var xElement in xDocument.Descendants("text"))
        {
            var value = xElement.Value;
            if (!string.IsNullOrEmpty(value) && value.Length > 0 && value[0] is var c && c == 0xFFFD)
            {
                // 0xFFFFD 是 utf8 特殊字符
                // 画出来就是�符号，不如删掉
                xElement.Value = string.Empty;

                anyUpdate = true;
            }
        }

        if (anyUpdate)
        {
            var convertedFile = Path.Join(workingFolder.FullName, $"FixSVG_{Path.GetRandomFileName()}.svg");
            xDocument.Save(convertedFile);
            return new FileInfo(convertedFile);
        }

        // 啥都不用改，返回原图
        return svgFile;
    }
}