using DocumentFormat.OpenXml.Drawing;
using DocumentFormat.OpenXml.Presentation;

namespace DocumentFormat.OpenXml.Flatten.Utils
{
    internal static class TextListStyleHelper
    {
        public static TextParagraphPropertiesType?[]
            ToTextParagraphPropertiesTypeList(this ListStyle textBodyListStyle)
        {
            return new TextParagraphPropertiesType?[]
            {
                textBodyListStyle.Level1ParagraphProperties,
                textBodyListStyle.Level2ParagraphProperties,
                textBodyListStyle.Level3ParagraphProperties,
                textBodyListStyle.Level4ParagraphProperties,
                textBodyListStyle.Level5ParagraphProperties,
                textBodyListStyle.Level6ParagraphProperties,
                textBodyListStyle.Level7ParagraphProperties,
                textBodyListStyle.Level8ParagraphProperties,
                textBodyListStyle.Level9ParagraphProperties
            };
        }

        public static TextParagraphPropertiesType?[]
            ToTextParagraphPropertiesTypeList(this TextListStyleType textListStyleType)
        {
            return new TextParagraphPropertiesType?[]
            {
                textListStyleType.Level1ParagraphProperties,
                textListStyleType.Level2ParagraphProperties,
                textListStyleType.Level3ParagraphProperties,
                textListStyleType.Level4ParagraphProperties,
                textListStyleType.Level5ParagraphProperties,
                textListStyleType.Level6ParagraphProperties,
                textListStyleType.Level7ParagraphProperties,
                textListStyleType.Level8ParagraphProperties,
                textListStyleType.Level9ParagraphProperties
            };
        }
    }
}
