#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 采用 <see cref="Emu"/> 表示的矩形
    /// </summary>
    public readonly struct EmuRectangle
    {
        public EmuRectangle(Emu left, Emu top, Emu right, Emu bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public EmuRectangle(EmuPoint point, EmuSize size)
        {
            Left = point.X;
            Top = point.Y;
            Right = new Emu(point.X.Value + size.Width.Value);
            Bottom = new Emu(point.Y.Value + size.Height.Value);
        }

        public Emu Left { get; }
        public Emu Top { get; }

        public Emu Right { get; }
        public Emu Bottom { get; }

        public Emu Width => new Emu(Right.Value - Left.Value);
        public Emu Height => new Emu(Bottom.Value - Top.Value);

        public EmuPoint LeftTop => new EmuPoint(Left, Top);
        public EmuPoint RightBottom => new EmuPoint(Right, Bottom);
        public EmuSize Size => new EmuSize(Width, Height);
    }
}