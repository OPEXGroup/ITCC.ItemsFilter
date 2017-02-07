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
using System.ComponentModel;

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    ///     Defines the greater or equal filter.
    /// </summary>
    /// <typeparam name="T">IComparable structure.</typeparam>
    public class GreaterOrEqualFilter<T> : LessOrEqualFilter<T>, IComparableFilter<T>
        where T : struct, IComparable
    {
        /// <summary>
        ///     Determines whether the specified target is a match.
        /// </summary>
        public override void IsMatch(FilterPresenter sender, FilterEventArgs e)
        {
            if (e.Accepted)
            {
                if (e.Item == null)
                    e.Accepted = false;
                else
                {
                    var value = getter(e.Item);
                    e.Accepted = value.CompareTo(_compareTo.Value) >= 0;
                }
            }
        }

        #region IComparableFilter Members

        object IComparableFilter.CompareTo
        {
            get { return CompareTo; }
            set { CompareTo = (T?) value; }
        }

        #endregion

        /// <summary>
        ///     Initializes a new instance of the <see cref="GreaterOrEqualFilter" /> class.
        /// </summary>
        /// <param name="getter">Func that return from item IComparable value to compare.</param>
        public GreaterOrEqualFilter(ItemPropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            Name = "Greater or equal:";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="GreaterOrEqualFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="compareTo">The compare to initial value.</param>
        public GreaterOrEqualFilter(ItemPropertyInfo propertyInfo, T compareTo)
            : this(propertyInfo)
        {
            base.CompareTo = compareTo;
        }
    }
}