using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 公式转换器
    /// </summary>
    class FormulaConverter
    {
        /// <summary>
        /// 创建公式转换器
        /// </summary>
        /// <param name="symbol"></param>
        /// <param name="argumentCount"></param>
        /// <param name="calculate"></param>
        public FormulaConverter(string symbol, int argumentCount, Func<FormulaCalculateContext, double> calculate)
        {
            Symbol = symbol;
            _calculate = calculate;
            ArgumentCount = argumentCount;
        }

        /// <summary>
        /// 符号
        /// </summary>
        public string Symbol { get; }

        /// <summary>
        /// 参数数量
        /// </summary>
        public int ArgumentCount { get; }

        private readonly Func<FormulaCalculateContext, double> _calculate;

        public double? Convert(FormulaConvertContext context)
        {
            if (!context.InputText.StartsWith(Symbol))
            {
                return default;
            }

            // 转换字符串
            // 然后通过字典读取到实际的值，调用 _calculate 进行计算
            // 计算的返回值存入字典和进行返回

            double[] arguments = new double[ArgumentCount];

            var formulaStringReader = new FormulaStringReader(context.InputText,
                // 忽略掉符号的长度，符号不作为内容本身加入计算
                Symbol.Length);

            for (int i = 0; i < ArgumentCount; i++)
            {
                var value = formulaStringReader.ReadNextValue();
                if (value is null)
                {
                    // 证明传入的逻辑不对
                    return default;
                }

                if (double.TryParse(value, out var number))
                {
                    arguments[i] = number;
                }
                else
                {
                    if (TryGetValue(value, context.VariablePool, out var n))
                    {
                        arguments[i] = n;
                    }
                    else
                    {
#if DEBUG
                        throw new ArgumentException($"找不到 {value} 的定义");
#else
                        // 证明传入的逻辑不对
                        return default;
#endif
                    }
                }
            }

            var result = _calculate(new FormulaCalculateContext()
            {
                Arguments = arguments
            });

            context.VariablePool[context.Name] = result;

            return result;
        }

        private static bool TryGetValue(string? value, Dictionary<string, double> contextVariablePool, out double n)
        {
            if (value is null)
            {
                n = 0;
                return false;
            }

            if (contextVariablePool.TryGetValue(value, out n))
            {
                return true;
            }

            var result = TryParseValue();
            if (result != null)
            {
                n = result.Value;
                contextVariablePool[value] = n;
                return true;
            }

            return false;

            double? TryParseValue()
            {
                if (value.Length < 3)
                {
                    // 不是 wd3,hd3 等，无法转换
                    return default;
                }

                var match = Regex.Match(value, @"(ss|w|h)d(\d+)");
                if (match.Success)
                {
                    if (double.TryParse(match.Groups[2].Value, out var divisor))
                    {
                        if (contextVariablePool.TryGetValue(match.Groups[1].Value, out var defined))
                        {
                            return defined / divisor;
                        }
                    }
                }

                return default;
            }
        }
    }
}
