// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System;

namespace ITCC.ItemsFilter.Model
{
    /// <summary>
    ///     Define the contract for compare property filter.
    /// </summary>
    public interface IComparableFilter : IPropertyFilter
    {
        /// <summary>
        ///     Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        object CompareTo { get; set; }
    }

    /// <summary>
    ///     Defines the contract for a compare filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComparableFilter<T> : IComparableFilter
        where T : struct, IComparable
    {
        /// <summary>
        ///     Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        new T? CompareTo { get; set; }
    }
}