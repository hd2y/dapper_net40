#if NET40
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

using System;

namespace System.Collections.Generic
{
    /// <summary>
    /// 表示键/值对的泛型只读集合。
    /// </summary>
    public interface IReadOnlyDictionary<TKey, TValue> : IReadOnlyCollection<KeyValuePair<TKey, TValue>>
    {
        /// <summary>
        /// 确定只读字典是否包含具有指定的键的元素
        /// </summary>
        bool ContainsKey(TKey key);

        /// <summary>
        /// 获取与指定键关联的值
        /// </summary>
        bool TryGetValue(TKey key, out TValue value);

        /// <summary>
        /// 获取只读字典中具有指定的键的元素
        /// </summary>
        TValue this[TKey key] { get; }

        /// <summary>
        /// 获取包含只读字典中的键的可枚举集合
        /// </summary>
        IEnumerable<TKey> Keys { get; }

        /// <summary>
        /// 获取包含只读字典中的值的可枚举集合
        /// </summary>
        IEnumerable<TValue> Values { get; }
    }
} 
#endif
