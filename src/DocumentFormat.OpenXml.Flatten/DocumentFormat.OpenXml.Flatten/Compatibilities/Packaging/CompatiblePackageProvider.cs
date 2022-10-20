using System.IO;
using System.IO.Packaging;

namespace DocumentFormat.OpenXml.Flatten.Compatibilities.Packaging;

/// <summary>
/// 带兼容处理的 Package 提供器
/// </summary>
public static class CompatiblePackageProvider
{
    /// <summary>
    /// 从传入的 <paramref name="s"/> 打开 <see cref="CompatiblePackage"/> 对象
    /// </summary>
    /// <param name="s"></param>
    /// <param name="packageFileMode"></param>
    /// <param name="packageFileAccess"></param>
    /// <returns></returns>
    public static Package OpenPackage(Stream s, FileMode packageFileMode,
        FileAccess packageFileAccess)
    {
        return CompatiblePackage.OpenPackage(s, packageFileMode, packageFileAccess);
    }
}
