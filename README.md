# DocumentFormat.OpenXml.Extensions

[中文](README.zh-cn.md) | [English](README.md)

The OpenXML SDK extensions. Including libraries and tools.

| Build |
|--|
|![](https://github.com/dotnet-campus/dotnetCampus.OfficeDocumentZipper/workflows/.NET%20Core/badge.svg)|

# Tools

## dotnetCampus.OfficeDocumentZipper

A dotnet tool to assist in editing Office document files

### Usage

```
dotnet tool update -g dotnetCampus.OfficeDocumentZipper

OfficeDocumentZipper
```

### Feature

- Unzip pptx docx xlsx file
- Zip directory to pptx docx xlsx file
- Convert OpenXML unit

![](https://user-images.githubusercontent.com/16054566/91013976-2b1c4580-e61b-11ea-8ef2-044ea79ef31b.png)

# Libraries

| Name | NuGet|
|--|--|
|dotnetCampus.OpenXMLUnitConverter|[![](https://img.shields.io/nuget/v/dotnetCampus.OpenXMLUnitConverter.svg)](https://www.nuget.org/packages/dotnetCampus.OpenXMLUnitConverter)|
|dotnetCampus.OpenXMLUnitConverter.Source|[![](https://img.shields.io/nuget/v/dotnetCampus.OpenXMLUnitConverter.Source.svg)](https://www.nuget.org/packages/dotnetCampus.OpenXMLUnitConverter.Source)|


## dotnetCampus.OpenXMLUnitConverter

Defining units for OpenXml properties and the unit conversion function.

### Install

DLL Pakcage:

```xml
<PackageReference Include="dotnetCampus.OpenXmlUnitConverter" Version="1.8.0" />
```

[SouceYard](https://github.com/dotnet-campus/SourceYard) Package:

```xml
<PackageReference Include="dotnetCampus.OpenXmlUnitConverter.Source" Version="1.8.0">
  <PrivateAssets>all</PrivateAssets>
  <IncludeAssets>runtime; build; native; contentfiles; analyzers</IncludeAssets>
</PackageReference>
```

### Usage

The sample:

```csharp
void Foo(DocumentFormat.OpenXml.Drawing.Point2DType point)
{
    var x = new Emu(point.X);
    var pixelValue = x.ToPixel();
    var cmValue = x.ToCm();
}
```

# Thanks

- [OfficeDev/Open-XML-SDK: Open XML SDK by Microsoft](https://github.com/OfficeDev/Open-XML-SDK/ )
- [ironfede/openmcdf: Microsoft Compound File .net component - pure C# - NET Standard 2.0](https://github.com/ironfede/openmcdf )

# Contributing

[![PRs Welcome](https://img.shields.io/badge/PRs-welcome-brightgreen.svg?style=flat-square)](https://github.com/dotnet-campus/DocumentFormat.OpenXml.Extensions/pulls)

If you would like to contribute, feel free to create a [Pull Request](https://github.com/dotnet-campus/DocumentFormat.OpenXml.Extensions/pulls), or give us [Bug Report](https://github.com/dotnet-campus/DocumentFormat.OpenXml.Extensions/issues/new).

# License

[![](https://img.shields.io/badge/License-MIT-blue?style=flat-square)](LICENSE)
