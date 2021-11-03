#pragma warning disable CS1591 // 缺少对公共可见类型或成员的 XML 注释

namespace dotnetCampus.OpenXmlUnitConverter
{
    /// <summary>
    /// 数值转换辅助类
    /// </summary>
    public static class UnitConverter
    {
        public const double DefaultDpi = 96;

        #region EmuPercentage

        public static PixelPercentage ToPixelPercentage(this EmuPercentage emuPercentage) =>
            new PixelPercentage((int)new Emu(emuPercentage.Value).ToPixel().Value);

        #endregion

        #region Pound

        public static Pound ToPound(this PoundHundredfold poundHundredfold) => new Pound(poundHundredfold.Value / 100);

        public static Pound ToPound(this Pixel pixel) => new Pound(pixel.Value / 96 * 72);

        public static PoundHundredfold ToPoundHundredfold(this Pound pound) => new PoundHundredfold(pound.Value * 100);

        public static Pixel ToPixel(this Pound pound) => new Pixel(pound.Value * 96 / 72);

        public static Pixel ToPixel(this PoundHundredfold pound) => pound.ToPound().ToPixel();

        #endregion

        #region PtHundredfold

        public static Pt ToPt(this PtHundredfold ptHundredfold) => new Pt(ptHundredfold.Value / 100);

        public static Pixel ToPixel(this PtHundredfold ptHundredfold) => ptHundredfold.ToPt().ToPixel();

        #endregion

        #region Pixel

        public static Pixel ToPixel(this Inch inch) => inch.ToEmu().ToPixel();

        public static Inch ToInch(this Pixel pixel) => pixel.ToEmu().ToInch();

        #endregion

        #region Dxa

        /// <summary>
        /// 将 <see cref="Dxa"/> 单位转换为 <see cref="Pt"/> 单位。
        /// </summary>
        public static Pt ToPt(this Dxa dxa)
        {
            return new Pt(dxa.Value / 20);
        }

        public static Dxa ToDxa(this Pt pt)
        {
            return new Dxa(pt.Value * 20);
        }

        public static Inch ToInch(this Dxa dxa)
        {
            return new Inch(dxa.Value / 72);
        }

        public static Dxa ToDxa(this Inch inch)
        {
            return new Dxa(inch.Value * 72);
        }

        #endregion

        #region Mm

        public static Cm ToCm(this Mm mm)
        {
            return new Cm(mm.Value / 10);
        }

        public static Mm ToMm(this Cm cm)
        {
            return new Mm(cm.Value * 10);
        }

        #endregion

        #region Pt

        public static Cm ToCm(this Pt pt)
        {
            return pt.ToEmu().ToCm();
        }

        public static Pt ToPt(this Cm cm)
        {
            return cm.ToEmu().ToPt();
        }

        public static Mm ToMm(this Pt pt)
        {
            return pt.ToCm().ToMm();
        }

        public static Pt ToPt(this Mm mm)
        {
            return mm.ToCm().ToPt();
        }

        public static Pt ToPt(this HalfPoint halfPoint)
        {
            return new Pt(halfPoint.Value / 2);
        }

        public static HalfPoint ToHalfPoint(this Pt pt)
        {
            return new HalfPoint(pt.Value * 2);
        }


        public static Pixel ToPixel(this Pt pt)
        {
            return new Pixel(pt.Value / 72 * DefaultDpi);
        }

        public static Pt ToPoint(this Pixel px)
        {
            return new Pt(px.Value * 72 / DefaultDpi);
        }

        #endregion

        #region Emu

        public static Emu ToEmu(this Inch inch)
        {
            return new Emu(inch.Value * 914400);
        }

        public static Inch ToInch(this Emu emu)
        {
            return new Inch(emu.Value / 914400);
        }

        public static Emu ToEmu(this Cm cm)
        {
            return new Emu(cm.Value * 360000);
        }

        public static Cm ToCm(this Emu emu)
        {
            return new Cm(emu.Value / 360000);
        }

        public static Emu ToEmu(this Mm cm)
        {
            return new Emu(cm.Value * 36000);
        }

        public static Dxa ToDxa(this Emu emu)
        {
            return new Dxa(emu.Value / 635);
        }

        public static Emu ToEmu(this Dxa dxa)
        {
            return new Emu(dxa.Value * 635);
        }

        public static Mm ToMm(this Emu emu)
        {
            return new Mm(emu.Value / 36000);
        }


        public static Emu ToEmu(this Pixel px)
        {
            return new Emu(px.Value * 914400 / DefaultDpi);
        }

        public static Pixel ToPixel(this Emu emu)
        {
            return new Pixel(emu.Value / 914400 * DefaultDpi);
        }

        public static Emu ToEmu(this Pt pt)
        {
            return new Emu(pt.Value * 12700);
        }

        public static Pt ToPt(this Emu emu)
        {
            return new Pt(emu.Value / 12700);
        }

        #endregion

        #region PixelPoint

        public static PixelPoint ToPixelPoint(this EmuPoint emuPoint) =>
            new PixelPoint(emuPoint.X.ToPixel(), emuPoint.Y.ToPixel());

        public static EmuPoint ToEmuPoint(this PixelPoint pixelPoint) =>
            new EmuPoint(pixelPoint.X.ToEmu(), pixelPoint.Y.ToEmu());

        #endregion

        #region PixelSize

        public static PixelSize ToPixelSize(this EmuSize emuPoint) =>
            new PixelSize(emuPoint.Width.ToPixel(), emuPoint.Height.ToPixel());

        public static EmuSize ToEmuSize(this PixelSize pixelPoint) =>
            new EmuSize(pixelPoint.Width.ToEmu(), pixelPoint.Height.ToEmu());

        #endregion
    }
}
