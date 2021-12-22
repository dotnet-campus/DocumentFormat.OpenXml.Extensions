using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace DocumentFormat.OpenXml.Flatten.Framework
{
    /// <summary>
    /// 带继承的对象，可以继承属性的值
    /// </summary>
    /// 如果对象有设置属性，那么采用对象设置的属性
    /// 如果对象没有设置属性，尝试从 Reserved 属性读取继承的属性
    /// 如果没有 Reserved 属性，证明就是最底层的对象，采用传入的默认属性值
    public class FlattenObject
    {
        /// <summary>
        /// 创建带继承的对象
        /// </summary>
        /// <param name="reserved"></param>
        public FlattenObject(FlattenObject? reserved = null)
        {
            Reserved = reserved;
        }

        private FlattenObject? Reserved { get; }
        private Dictionary<string, object> ValueDictionary { get; } = new Dictionary<string, object>();

        /// <summary>
        /// 设置属性值
        /// </summary>
        /// <param name="value"></param>
        /// <param name="propertyName"></param>
        protected void SetValue(object value, [CallerMemberName] string propertyName = null!)
        {
            ValueDictionary[propertyName] = value;
        }

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected T? GetValue<T>([CallerMemberName] string propertyName = null!)
            => GetValue<T>(default!, propertyName);

        /// <summary>
        /// 获取属性值
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="defaultValue"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected T GetValue<T>(T defaultValue, [CallerMemberName] string propertyName = null!)
        {
            if (ValueDictionary.TryGetValue(propertyName, out var value))
            {
                return (T) value;
            }
            else
            {
                if (Reserved is not null)
                {
                    return Reserved.GetValue<T>(defaultValue, propertyName);
                }
                else
                {
                    return defaultValue;
                }
            }
        }
    }
}
