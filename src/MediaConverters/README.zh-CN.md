# DotNetCampus.MediaConverters

## 用法

### 命令行

命令行参数：

```shell
--WorkingFolder: 工作目录
--InputFile: 输入文件路径
--OutputFile: 输出文件路径
--ConvertConfigurationFile: 转换配置文件路径
```

其中 `--ConvertConfigurationFile` 转换配置文件是一个 Json 格式的文件，里面包含转换的任务的配置内容。配置内容格式为 ImageConvertContext 类型的序列化内容，具体定义如下：

- MaxImageWidth: 最大图片宽度限制。可不填或为空，表示不限制
- MaxImageHeight: 最大图片高度限制。可不填或为空，表示不限制
- UseAreaSizeLimit: 是否使用面积大小限制。可不填或为空表示默认值，默认为使用面积大小限制
- ImageConvertTaskList: 转换任务列表。可不填或为空，表示不进行转换任务。其类型为 IImageConvertTask 接口的数组

其接口固定有 Type 属性，表示任务类型。具体的任务类型及其参数如下：

- SetSoftEdgeEffectTask： 设置柔化边缘效果任务
  - Radius: 边缘半径，单位为像素。可不填或为空

```json
{
  "ImageConvertTaskList":
  [
    {
      "Type": "SetSoftEdgeEffectTask",
      "Radius": 20
    }
  ]
}
```

- SetLuminanceEffectTask： 设置冲蚀效果

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetLuminanceEffectTask"
    }
  ]
}
```

- SetGrayScaleEffectTask： 设置灰度图效果

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetGrayScaleEffectTask"
    }
  ]
}
```

- SetContrastTask: 更改当前图像的对比度
  - Percentage: 值为 0 将创建一个完全灰色的图像。值为 1 时输入保持不变。其他值是效果的线性乘数。允许超过 1 的值，从而提供具有更高对比度的结果

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetContrastTask",
      "Percentage": 0.7
    }
  ]
}
```

- SetBrightnessTask： 更改当前图像的亮度
  -  Percentage: 值为 0 将创建一个完全黑色的图像。值为 1 时输入保持不变。其他值是效果的线性乘数。允许超过 1 的值，从而提供更明亮的结果

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetBrightnessTask",
      "Percentage": 0.7
    }
  ]
}
```

- SetBlackWhiteEffectTask： 设置黑白图效果

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetBlackWhiteEffectTask",
      "Threshold": 0.7
    }
  ]
}
```

- SetDuotoneEffectTask： 设置双色调效果
  - ArgbFormatColor1: 颜色 1 的 ARGB 格式字符串
  - ArgbFormatColor2: 颜色 2 的 ARGB 格式字符串

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "SetDuotoneEffectTask",
      "ArgbFormatColor1": "#FFF1D7A6",
      "ArgbFormatColor2": "#FFFFF2C8"
    }
  ]
}
```

- ReplaceColorTask： 替换颜色任务
  - ReplaceColorInfoList: 替换颜色信息列表。每个替换颜色信息包含以下属性：
    - OldColor: 旧颜色的 ARGB 格式字符串
    - NewColor: 新颜色的 ARGB 格式字符串

```json
{
  "ImageConvertTaskList": 
  [
    {
      "Type": "ReplaceColorTask",
      "ReplaceColorInfoList": 
      [
        {
          "OldColor": "#FFF1D7A6",
          "NewColor": "#00FFFFFF"
        },
        {
          "OldColor": "#FFFFF2C8",
          "NewColor": "#00FFFFFF"
        },
        {
          "OldColor": "#FFE3D8AB",
          "NewColor": "#00FFFFFF"
        }
      ]
    }
  ]
}
```

## 版权须知

如您使用 MediaConverters.Lib 作为直接依赖库，则您必须遵守 [Six Labors Split License, Version 1.0](ThirdPartyNotices/SixLabors.LICENSE.txt) 协议。这是因为本项目采用了 Six Labors 的 ImageSharp 库作为基础设施的原因

本项目的其他部分均采用 MIT 协议发布，您可以自由使用、更改、重新分发，且无需付费，商业许可免费使用，无版权纠纷

满足以下**任一**条件，您可放心在商业项目中使用本项目，而无需付费以及任何涉及版权的要求：

- 仅通过进程调用的命令行工具方式使用 DotNetCampus.MediaConverter 系列工具；而非作为直接依赖库的方式使用
  - 注： 这是因为按照 Six Labors 的协议，本工具属于开源项目，符合 Six Labors 免费条件。通过命令行方式使用工具时，不属于对 Six Labors 的依赖，无需购买 Six Labors 商业许可
  - 注： 上述说明来自于 Six Labors 的 CEO —— James Jackson-South 的答复。具体答复内容引用如下：
  - > If they are just using your tool as it is, they do not need to purchase a separate license.
  - 参阅： <https://sixlabors.freshdesk.com/support/tickets/517> (此链接无法直接被访问，仅用于与 Six Labors 组织沟通时附带)
- 开源项目
- 年总收入少于 100 万美元的营利性公司或个人

反之，若您不满足上述任一条件，则需要购买 Six Labors 的商业许可