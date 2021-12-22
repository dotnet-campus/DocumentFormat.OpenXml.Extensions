using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 公式转换的上下文信息
    /// </summary>
    public struct FormulaConvertContext
    {
        /// <summary>
        /// 表达式名，对应 OpenXML 的 &lt;a:gd name="T0" fmla="*/ 7 w 18" /&gt; 中的 name 属性
        /// </summary>
        public string Name { set; get; }
        /// <summary>
        /// 输入的表达式，对应 OpenXML 的 &lt;a:gd name="T0" fmla="*/ 7 w 18" /&gt; 中的 fmla 属性
        /// </summary>
        public string InputText { set; get; }

        /// <summary>
        /// 变量池
        /// </summary>
        public Dictionary<string, double> VariablePool { set; get; }
    }
}
