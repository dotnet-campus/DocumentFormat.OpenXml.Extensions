# DocumentFormat.OpenXml.Flatten

将 OpenXML 里面的 PPT 元素继承的属性拍平，让元素可以获取到最终的属性值。属性继承顺序是，先找元素本身，再找 SlideLayout 再找 SlideMaster 再找 Theme 如果依然找不到，就使用放在 App 里面的默认值

| Build |
|--|
|![](https://github.com/dotnet-campus/dotnetCampus.OfficeDocumentZipper/workflows/.NET%20Core/badge.svg)|

| Name | NuGet|
|--|--|
|dotnetCampus.DocumentFormat.OpenXml.Flatten|[![](https://img.shields.io/nuget/v/dotnetCampus.DocumentFormat.OpenXml.Flatten.svg)](https://www.nuget.org/packages/dotnetCampus.DocumentFormat.OpenXml.Flatten)|
|dotnetCampus.DocumentFormat.OpenXml.Flatten.Source|[![](https://img.shields.io/nuget/v/dotnetCampus.DocumentFormat.OpenXml.Flatten.Source.svg)](https://www.nuget.org/packages/dotnetCampus.DocumentFormat.OpenXml.Flatten.Source)|

## 功能

- 对形状元素的属性样式的继承处理
- 提供预设形状和自定义形状的 Path 转为 WPF 的 [Mini-Language](https://docs.microsoft.com/en-us/dotnet/desktop/wpf/graphics-multimedia/path-markup-syntax?view=netframeworkdesktop-4.8) 字符串，此字符串和 SVG 兼容
- 提供 PPT 内嵌 OLE 或 XLSX 格式的表格的内容读取