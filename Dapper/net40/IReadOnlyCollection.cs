#if NET40
using System;
using System.Runtime.CompilerServices;

namespace System.Collections.Generic
{
    /// <summary>
    /// 表示元素的强类型化只读集合。
    /// </summary>
    public interface IReadOnlyCollection<out T> : IEnumerable<T>
    {
        /// <summary>
        /// 获取集合中的元素数。
        /// </summary>
        int Count { get; }
    }
} 
#endif
