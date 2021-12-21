namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 形状的几何公式计算上下文
    /// </summary>
    public struct FormulaCalculateContext
    {
        /// <summary>
        /// 形状的几何公式计算参数
        /// </summary>
        public double[] Arguments { set; get; }

        /// <summary>
        /// 表示第一个参数
        /// </summary>
        public double X => Arguments[0];

        /// <summary>
        /// 表示第二个参数
        /// </summary>
        public double Y => Arguments[1];

        /// <summary>
        /// 表示第三个参数
        /// </summary>
        public double Z => Arguments[2];
    }
}
