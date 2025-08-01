# DotNetCampus.MediaConverters

## Usage

### Command Line

Command line parameters:

```shell
--WorkingFolder: Working directory
--InputFile: Path to the input file
--OutputFile: Path to the output file
--ConvertConfigurationFile: Path to the conversion configuration file
```

The `--ConvertConfigurationFile` parameter specifies a JSON-format configuration file, which contains the settings for the conversion tasks. The configuration follows the structure of a serialized `ImageConvertContext` object, defined as follows:

- **MaxImageWidth**: Maximum image width limit. Optional; if omitted or empty, no limit is applied.
- **MaxImageHeight**: Maximum image height limit. Optional; if omitted or empty, no limit is applied.
- **UseAreaSizeLimit**: Whether to apply an area size limit. Optional; if omitted or empty, the default is to use an area size limit.
- **ImageConvertTaskList**: List of conversion tasks. Optional; if omitted or empty, no conversion tasks will be performed. This should be an array of objects implementing the `IImageConvertTask` interface.

Each task object must contain a `Type` property indicating the type of task. The available task types and their parameters are as follows:

---

#### SetSoftEdgeEffectTask

Applies a soft edge effect to the image.

- **Radius**: The radius of the soft edge in pixels. Optional.

Example:

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

---

#### SetLuminanceEffectTask

Applies a luminance (erosion) effect to the image.

Example:

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

---

#### SetGrayScaleEffectTask

Converts the image to grayscale.

Example:

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

---

#### SetContrastTask

Adjusts the contrast of the current image.

- **Percentage**: A value of 0 produces a completely gray image. A value of 1 leaves the input unchanged. Other values act as linear multipliers for contrast adjustment. Values greater than 1 are allowed for increased contrast.

Example:

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

---

#### SetBrightnessTask

Adjusts the brightness of the current image.

- **Percentage**: A value of 0 produces a completely black image. A value of 1 leaves the input unchanged. Other values act as linear multipliers for brightness adjustment. Values greater than 1 are allowed for increased brightness.

Example:

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

---

#### SetBlackWhiteEffectTask

Converts the image to black and white.

- **Threshold**: Threshold value for black-and-white conversion (optional).

Example:

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

---

#### SetDuotoneEffectTask

Applies a duotone effect using two specified colors.

- **ArgbFormatColor1**: ARGB format string representing color 1.
- **ArgbFormatColor2**: ARGB format string representing color 2.

Example:

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

---

#### ReplaceColorTask

Replaces specific colors in the image with new ones.

- **ReplaceColorInfoList**: A list containing color replacement information. Each entry includes:
   - **OldColor**: ARGB format string for the color to be replaced.
   - **NewColor**: ARGB format string for the replacement color.

Example:

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

## Copyright Notice

If you use MediaConverters.Lib as a direct dependency, you must comply with the [Six Labors Split License, Version 1.0](ThirdPartyNotices/SixLabors.LICENSE.txt). This is because this project uses Six Labors' ImageSharp library as its infrastructure.

All other parts of this project are released under the MIT License. You are free to use, modify, and redistribute them without charge. Commercial use is free, and there are no copyright disputes.

You can use this project in commercial projects without payment or any copyright requirements if you meet **any** of the following conditions:

- You use the DotNetCampus.MediaConverter tools only via command-line invocation as a separate process, not as a direct dependency library.
  - Note: According to the Six Labors license, this tool is open source and meets the free usage conditions of Six Labors. Using the tool via command-line does not constitute a dependency on Six Labors, so you do not need to purchase a commercial license from Six Labors.
  - Note: The above explanation is based on a response from James Jackson-South, CEO of Six Labors. The specific reply is quoted as follows:
  - > If they are just using your tool as it is, they do not need to purchase a separate license.
  - Reference: <https://sixlabors.freshdesk.com/support/tickets/517> (This link may not be directly accessible and is provided for communication with the Six Labors organization.)
- The project is open source.
- The annual revenue of the for-profit company or individual is less than $1,000,000 USD.

Otherwise, if you do not meet any of the above conditions, you will need to purchase a commercial license from Six Labors.

## Thanks

- [Six Labors](https://sixlabors.com/) for providing the ImageSharp library, which is used as the infrastructure for this project.
- [wieslawsoltes/wmf](https://github.com/wieslawsoltes/wmf)