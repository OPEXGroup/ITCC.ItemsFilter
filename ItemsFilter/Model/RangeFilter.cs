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
using System.Diagnostics;
using BolapanControl.ItemsFilter.View;

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    ///     Defines the range filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof (RangeFilterView))]
    public class RangeFilter<T> : PropertyFilter, IRangeFilter<T>
        where T : struct, IComparable
    {
        /// <summary>
        ///     Get or set the minimum value used in the comparison.
        ///     If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareFrom
        {
            get { return _compareFrom; }
            set
            {
                if (!Equals(_compareFrom, value))
                {
                    _compareFrom = value;
                    RefreshIsActive();
                    OnIsActiveChanged();
                    RaisePropertyChanged("CompareFrom");
                }
            }
        }

        /// <summary>
        ///     Get or set the maximum value used in the comparison.
        ///     If CompareFrom and CompareTo is null, filter deactivated.
        /// </summary>
        public T? CompareTo
        {
            get { return _compareTo; }
            set
            {
                if (!Equals(_compareTo, value))
                {
                    _compareTo = value;
                    RefreshIsActive();
                    OnIsActiveChanged();
                    RaisePropertyChanged("CompareTo");
                }
            }
        }

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
                    e.Accepted = (ReferenceEquals(_compareFrom, null) | value.CompareTo(_compareFrom) >= 0)
                                 && (ReferenceEquals(_compareTo, null) | value.CompareTo(_compareTo) <= 0);
                }
            }
        }

        private T? _compareFrom;
        private T? _compareTo;
        private readonly Func<object, T> getter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="getter">Func that return from item values to compare.</param>
        protected RangeFilter(Func<object, T> getter)
        {
            Debug.Assert(getter != null, "getter is null.");
            this.getter = getter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo)
        {
            //if (!typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            Debug.Assert(propertyInfo != null, "propertyInfo is null.");
            Debug.Assert(typeof (IComparable).IsAssignableFrom(propertyInfo.PropertyType),
                "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            PropertyInfo = propertyInfo;
            Func<object, object> getterItem = ((PropertyDescriptor) PropertyInfo.Descriptor).GetValue;
            getter = t => (T) getterItem(t);
            Name = "In range:";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="CompareFrom">Minimum value.</param>
        /// <param name="CompareTo">Maximum value.</param>
        public RangeFilter(ItemPropertyInfo propertyInfo, T CompareFrom, T CompareTo)
            : this(propertyInfo)
        {
            _compareTo = CompareTo;
            _compareFrom = CompareFrom;
            RefreshIsActive();
        }

        /// <summary>
        ///     Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged()
        {
            if (!IsActive)
            {
                CompareFrom = null;
                CompareTo = null;
            }
            base.OnIsActiveChanged();
        }

        private void RefreshIsActive()
        {
            IsActive = !(ReferenceEquals(_compareFrom, null) && ReferenceEquals(_compareTo, null));
        }

        #region IRangeFilter Members

        object IRangeFilter.CompareFrom
        {
            get { return CompareFrom; }
            set { CompareFrom = (T?) value; }
        }

        object IRangeFilter.CompareTo
        {
            get { return CompareFrom; }
            set { CompareFrom = (T?) value; }
        }

        #endregion
    }
}