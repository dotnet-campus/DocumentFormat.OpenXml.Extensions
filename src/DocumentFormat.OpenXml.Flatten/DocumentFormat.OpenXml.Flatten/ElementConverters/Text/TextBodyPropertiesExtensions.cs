using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DocumentFormat.OpenXml.Flatten.Contexts;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.Text
{
    /// <summary>
    /// TextBody 的属性扩展方法
    /// </summary>
    public static class TextBodyPropertiesExtensions
    {
        /// <summary>
        /// 获取文本相对形状的边距，将会使用 lIns tIns rIns bIns 属性
        /// </summary>
        /// <param name="bodyProperties"></param>
        /// <returns></returns>
        public static EmuTextMargin GetTextMargin(this DocumentFormat.OpenXml.Drawing.BodyProperties? bodyProperties)
        {
            if (bodyProperties is null)
            {
                return default;
            }
            // 可参考 Ecma-376 Ecma Office Open XML Part 1 - Fundamentals And Markup Language Reference 的 20.4.2.22 章内容
            // lIns tIns rIns bIns存在默认值
            var leftInset = bodyProperties.LeftInset ?? 91440d;
            var topInset = bodyProperties.TopInset ?? 45720d;
            var rightInset = bodyProperties.RightInset ?? 91440d;
            var bottomInset = bodyProperties.BottomInset ?? 45720d;

            var emuTextMargin = new EmuTextMargin(leftInset, topInset, rightInset, bottomInset);
            return emuTextMargin;
        }
    }
}
