# DocumentFormat.OpenXml.Extensions

[中文](README.zh-cn.md) | [English](README.md)

OpenXML SDK 的扩展集，包括扩展库和辅助开发工具

| Build |
|--|
|![](https://github.com/dotnet-campus/dotnetCampus.OfficeDocumentZipper/workflows/.NET%20Core/badge.svg)|

# 工具

## dotnetCampus.OfficeDocumentZipper

[解压缩文档为文件夹工具](https://blog.lindexi.com/post/dotnet-OpenXML-%E8%A7%A3%E5%8E%8B%E7%BC%A9%E6%96%87%E6%A1%A3%E4%B8%BA%E6%96%87%E4%BB%B6%E5%A4%B9%E5%B7%A5%E5%85%B7.html)

在开发过程中，需要不断解压缩 Office 文档，阅读或修改文档的内容，再次压缩回 Office 文档，这样的效率比较低。本工具提供一键解压自动格式化，一键组装Office文档且打开的功能

### 使用方法

OfficeDocumentZipper 工具作为 dotnet tool 发布，可使用以下代码进行安装和启动

```
dotnet tool update -g dotnetCampus.OfficeDocumentZipper

OfficeDocumentZipper
```

建议将以上命令存放作为 bat 脚本，方便每次快速运行

### 功能

- 解压 pptx docx xlsx 文件以及自动格式化文档内容
- 压缩文件夹作为 pptx docx xlsx 文件
- 转换 OpenXML 单位

![](https://user-images.githubusercontent.com/16054566/91013976-2b1c4580-e61b-11ea-8ef2-044ea79ef31b.png)

# 库

| Name | NuGet|
|--|--|
|dotnetCampus.OpenXMLUnitConverter|[![](https://img.shields.io/nuget/v/dotnetCampus.OpenXMLUnitConverter.svg)](https://www.nuget.org/packages/dotnetCampus.OpenXMLUnitConverter)|
|dotnetCampus.OpenXMLUnitConverter.Source|[![](https://img.shields.io/nuget/v/dotnetCampus.OpenXMLUnitConverter.Source.svg)](https://www.nuget.org/packages/dotnetCampus.OpenXMLUnitConverter.Source)|


## dotnetCampus.OpenXMLUnitConverter

定义 OpenXML 的单位以及提供单位转换的功能

### 安装方法

DLL 包:

```xml
<PackageReference Include="dotnetCampus.OpenXmlUnitConverter" Version="1.8.0" />
```

[SouceYard](https://github.com/dotnet-campus/SourceYard) 源代码包:

```xml
<PackageReference Include="dotnetCampus.OpenXmlUnitConverter.Source" Version="1.8.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

### 使用方法

例子：

```csharp
void Foo(DocumentFormat.OpenXml.Drawing.Point2DType point)
{
    var x = new Emu(point.X);
    var pixelValue = x.ToPixel();
    var cmValue = x.ToCm();
}
```

详细请看 [Office Open XML 的测量单位](https://blog.lindexi.com/post/Office-Open-XML-%E7%9A%84%E6%B5%8B%E9%87%8F%E5%8D%95%E4%BD%8D.html )

# 感谢

- [OfficeDev/Open-XML-SDK: Open XML SDK by Microsoft](https://github.com/OfficeDev/Open-XML-SDK/ )
- [ironfede/openmcdf: Microsoft Compound File .net component - pure C# - NET Standard 2.0](https://github.com/ironfede/openmcdf )

# 开源社区

如果你希望参与贡献，欢迎 [Pull Request](https://github.com/dotnet-campus/DocumentFormat.OpenXml.Extensions/pulls)，或给我们 [报告 Bug](https://github.com/dotnet-campus/DocumentFormat.OpenXml.Extensions/issues/new)

# 授权协议

[![](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](LICENSE)