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

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    ///     Defines the contract for a range filter.
    /// </summary>
    public interface IRangeFilter : IFilter
    {
        /// <summary>
        ///     Gets or sets the minimum value.
        /// </summary>
        object CompareFrom { get; set; }

        /// <summary>
        ///     Gets or sets the maximum value.
        /// </summary>
        object CompareTo { get; set; }
    }

    /// <summary>
    ///     Defines the contract for a range filter.
    /// </summary>
    /// <typeparam name="T">Comparable value Type.</typeparam>
    public interface IRangeFilter<T> : IRangeFilter //, INotifyPropertyChanged
        where T : struct, IComparable
    {
        /// <summary>
        ///     Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        new T? CompareFrom { get; set; }

        /// <summary>
        ///     Gets or sets the maximum value.
        /// </summary>
        /// <value>To.</value>
        new T? CompareTo { get; set; }
    }
}