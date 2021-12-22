namespace DocumentFormat.OpenXml.Flatten.ElementConverters.FormulaCalculators
{
    /// <summary>
    /// 公式的字符串读取器
    /// </summary>
    public struct FormulaStringReader
    {
        /// <summary>
        /// 创建公式的字符串读取器
        /// </summary>
        /// <param name="inputText"></param>
        /// <param name="currentOffset"></param>
        public FormulaStringReader(string inputText, int currentOffset = 0)
        {
            _inputText = inputText;
            _currentOffset = currentOffset;
        }

        /// <summary>
        /// 读取下一个值
        /// </summary>
        /// <returns></returns>
        public string? ReadNextValue()
        {
            if (_currentOffset >= _inputText.Length)
            {
                return null;
            }

            var i = _currentOffset;

            for (; i < _inputText.Length; i++)
            {
                if (_inputText[i] != ' ')
                {
                    // 读取掉一些空格
                    break;
                }
            }

            _currentOffset = i;

            for (; i < _inputText.Length; i++)
            {
                if (_inputText[i] == ' ')
                {
                    break;
                }
            }

            var length = i - _currentOffset;
            if (length == 0)
            {
                return null;
            }

            var result = _inputText.Substring(_currentOffset, length);

            _currentOffset = i;

            return result;
        }

        private readonly string _inputText;
        private int _currentOffset;
    }
}
