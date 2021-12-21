using System.Collections.Generic;

using DocumentFormat.OpenXml.Flatten.ElementConverters.CommonElement;
using DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters;

using dotnetCampus.OpenXmlUnitConverter;

using static DocumentFormat.OpenXml.Flatten.ElementConverters.ShapeGeometryConverters.ShapeGeometryFormulaHelper;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 形状几何公式计算器
    /// </summary>
    public class ShapeGeometryFormulaCalculator
    {
        /// <summary>
        /// 形状几何公式计算器
        /// </summary>
        /// <param name="size">形状的尺寸，根据此尺寸计算出 Path 内容</param>
        public ShapeGeometryFormulaCalculator(EmuSize size)
        {
            var (h, w, l, r, t, b, hd2, hd4, hd5, hd6, hd8, ss, hc, vc, ls, ss2, ss4, ss6, ss8, wd2, wd4, wd5, wd6, wd8,
                wd10, cd2, cd4, cd6, cd8) = ShapeGeometryBase.GetFormulaProperties(size);

            _variablePool = new Dictionary<string, double>()
            {
                {nameof(h), h},
                {nameof(w), w},
                {nameof(l), l},
                {nameof(r), r},
                {nameof(t), t},
                {nameof(b), b},
                {nameof(hd2), hd2},
                {nameof(hd4), hd4},
                {nameof(hd5), hd5},
                {nameof(hd6), hd6},
                {nameof(hd8), hd8},
                {nameof(ss), ss},
                {nameof(hc), hc},
                {nameof(vc), vc},
                {nameof(ls), ls},
                {nameof(ss2), ss2},
                {nameof(ss4), ss4},
                {nameof(ss6), ss6},
                {nameof(ss8), ss8},
                {nameof(wd2), wd2},
                {nameof(wd4), wd4},
                {nameof(wd5), wd5},
                {nameof(wd6), wd6},
                {nameof(wd8), wd8},
                {nameof(wd10), wd10},
                {nameof(cd2), cd2},
                {nameof(cd4), cd4},
                {"3cd4", 3 * cd4},
                {nameof(cd6), cd6},
                {nameof(cd8), cd8},
                {"3cd8", 3 * cd8},
                {"7cd8", 7 * cd8},
            };
        }

        /// <summary>
        /// 给定公式，获取公式对应的值
        /// </summary>
        /// <param name="formula"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public Emu GetEmuValue(string? formula, double defaultValue = 0)
        {
            var value = Calculate(string.Empty, formula, defaultValue);
            return new Emu(value);
        }

        /// <summary>
        /// 进行计算公式
        /// </summary>
        /// <param name="name"></param>
        /// <param name="formula"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public double Calculate(string name, string? formula, double defaultValue = 0)
        {
            if (string.IsNullOrEmpty(formula))
            {
                return defaultValue;
            }

            if (double.TryParse(formula, out var variable))
            {
                return variable;
            }
            else if (_variablePool.TryGetValue(formula!, out variable))
            {
                return variable;
            }

            var formulaConvertContext = new FormulaConvertContext()
            {
                InputText = formula!,
                VariablePool = _variablePool,
                Name = name,
            };

            foreach (var formulaConverter in FormulaConverterList)
            {
                var value = formulaConverter.Convert(formulaConvertContext);
                if (value is not null)
                {
                    return value.Value;
                }
            }

            return defaultValue;
        }

        private readonly Dictionary<string, double> _variablePool;

        private static FormulaConverter[] FormulaConverterList { get; } = new FormulaConverter[]
        {
            new("val", 1, c => c.X),
            new("+-", 3, c => c.X + c.Y - c.Z),
            new("*/", 3, c => c.X * c.Y / c.Z),
            new("+/", 3, c => (c.X + c.Y) / c.Z),
            new("?:", 3, c => c.X > 0 ? c.Y : c.Z),
            new("abs", 1, c => Abs(c.X)),
            new("at2", 2, c => ATan2(c.X, c.Y)),
            new("cat2", 3, c => Cat2(c.X, c.Y, c.Z)),
            new("cos", 2, c => Cos(c.X, (int) c.Y)),
            new("sat2", 3, c => Sat2(c.X, c.Y, c.Z)),
            new("sin", 2, c => Sin(c.X, (int) c.Y)),
            new("tan", 2, c => Tan(c.X, (int) c.Y)),
            new("max", 2, c => System.Math.Max(c.X, c.Y)),
            new("min", 2, c => System.Math.Min(c.X, c.Y)),
            new("mod", 3, c => Mod(c.X, c.Y, c.Z)),
            new("pin", 3, c => Pin(c.X, c.Y, c.Z)),
            new("sqrt", 1, c => System.Math.Sqrt(c.X)),
            new("", 1, c => c.X)
        };
    }
}
