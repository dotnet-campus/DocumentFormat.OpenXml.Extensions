namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 采用 <see cref="Emu"/> 表示的点
    /// </summary>
    public readonly struct EmuPoint
    {
        /// <summary>
        /// 创建 <see cref="Emu"/> 表示的点
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public EmuPoint(Emu x, Emu y)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// 表示 X 坐标
        /// </summary>
        public Emu X { get; }

        /// <summary>
        /// 表示 Y 坐标
        /// </summary>
        public Emu Y { get; }
    }
}