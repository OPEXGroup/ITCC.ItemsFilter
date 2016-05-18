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
    ///     Define LessOrEqual filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [View(typeof (ComparableFilterView))]
    public class LessOrEqualFilter<T> : PropertyFilter, IComparableFilter<T>
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
                    e.Accepted = value.CompareTo(_compareTo) <= 0;
                }
            }
        }

        /// <summary>
        ///     Get or set the value used in the comparison. If assign null, filter deactivated.
        /// </summary>
        public T? CompareTo
        {
            get { return _compareTo; }
            set
            {
                if (!ReferenceEquals(_compareTo, value))
                {
                    _compareTo = value;
                    RefreshIsActive();
                    RaiseFilterChanged();
                    RaisePropertyChanged("CompareTo");
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

        protected readonly Func<object, T> getter;
        protected T? _compareTo;

        /// <summary>
        ///     Initializes a new instance of the <see cref="EqualFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="getter">Func that return from item IComparable value to compare.</param>
        protected LessOrEqualFilter(Func<object, T> getter)
        {
            Debug.Assert(getter != null, "getter is null.");
            this.getter = getter;
        }


        /// <summary>
        ///     Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public LessOrEqualFilter(ItemPropertyInfo propertyInfo)
        {
            //if (!typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            Debug.Assert(propertyInfo != null, "propertyInfo is null.");
            Debug.Assert(typeof (IComparable).IsAssignableFrom(propertyInfo.PropertyType),
                "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            PropertyInfo = propertyInfo;

            Func<object, object> getterItem = ((PropertyDescriptor) PropertyInfo.Descriptor).GetValue;
            getter = t => (T) getterItem(t);
            Name = "Less or equal:";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="LessOrEqualFilter&lt;T&gt;" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="compareTo">The compare to.</param>
        public LessOrEqualFilter(ItemPropertyInfo propertyInfo, T compareTo)
            : this(propertyInfo)
        {
            _compareTo = compareTo;
            RefreshIsActive();
        }

        /// <summary>
        ///     Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged()
        {
            if (!IsActive)
                CompareTo = null;
            base.OnIsActiveChanged();
        }

        private void RefreshIsActive()
        {
            IsActive = !ReferenceEquals(_compareTo, null);
        }
    }
}