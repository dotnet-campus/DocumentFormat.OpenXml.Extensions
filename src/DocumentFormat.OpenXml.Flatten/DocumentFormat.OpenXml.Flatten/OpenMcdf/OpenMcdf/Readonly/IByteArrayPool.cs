// ReSharper disable once CheckNamespace 特别的命名空间

namespace OpenMcdf
{
    /// <summary>
    /// 二进制数组缓存池
    /// </summary>
    public interface IByteArrayPool
    {
        /// <summary>
        /// 租借数组，将会返回大于或等于 <paramref name="minimumLength"/> 长度的二进制数组，数组里面也许存在内容
        /// </summary>
        /// <param name="minimumLength"></param>
        /// <returns></returns>
        byte[] Rent(int minimumLength);

        /// <summary>
        /// 归还数组
        /// </summary>
        /// <param name="byteList"></param>
        void Return(byte[] byteList);
    }
}
