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
    ///     Defines a string filter
    /// </summary>
    [View(typeof (StringFilterView))]
    public class StringFilter : PropertyFilter, IStringFilter
    {
        /// <summary>
        ///     Gets or sets the string filter mode.
        /// </summary>
        /// <value>The mode.</value>
        public StringFilterMode Mode
        {
            get { return _filterMode; }
            set
            {
                if (_filterMode != value)
                {
                    _filterMode = value;
                    OnIsActiveChanged();
                    RaisePropertyChanged("Mode");
                }
            }
        }

        /// <summary>
        ///     Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return _value; }
            set
            {
                if (_value != value)
                {
                    _value = value;
                    IsActive = !string.IsNullOrEmpty(value);
                    OnIsActiveChanged();
                    RaisePropertyChanged("Value");
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
                    var toCompare = getter(e.Item);
                    if (string.IsNullOrEmpty(toCompare))
                        e.Accepted = false;
                    else
                        switch (_filterMode)
                        {
                            case StringFilterMode.Contains:
                                e.Accepted = toCompare.Contains(_value);
                                break;
                            case StringFilterMode.StartsWith:
                                e.Accepted = toCompare.StartsWith(_value);
                                break;
                            case StringFilterMode.EndsWith:
                                e.Accepted = toCompare.EndsWith(_value);
                                break;
                            default:
                                e.Accepted = toCompare.Equals(_value);
                                break;
                        }
                }
            }
        }

        private StringFilterMode _filterMode = StringFilterMode.Contains;
        private string _value;

        private readonly Func<object, string> getter;

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringFilter" /> class.
        /// </summary>
        /// <param name="getter">Func that return from item string value to compare.</param>
        protected StringFilter(Func<object, string> getter)
        {
            Debug.Assert(getter != null, "getter is null.");
            this.getter = getter;
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringFilter" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        public StringFilter(ItemPropertyInfo propertyInfo, StringFilterMode filterMode = StringFilterMode.Contains)
        {
            //if (!typeof(string).IsAssignableFrom(propertyInfo.PropertyType))
            //    throw new ArgumentOutOfRangeException("propertyInfo", "typeof(string).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            Debug.Assert(propertyInfo != null, "propertyInfo is null.");
            Debug.Assert(typeof (IComparable).IsAssignableFrom(propertyInfo.PropertyType),
                "The typeof(IComparable).IsAssignableFrom(propertyInfo.PropertyType) return False.");
            PropertyInfo = propertyInfo;
            _filterMode = filterMode;
            Func<object, object> getterItem = ((PropertyDescriptor) PropertyInfo.Descriptor).GetValue;
            getter = t => (string) getterItem(t);
            Name = "String";
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="StringFilter" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="value">The value.</param>
        public StringFilter(ItemPropertyInfo propertyInfo, StringFilterMode filterMode, string value)
            : this(propertyInfo, filterMode)
        {
            _value = value;
            IsActive = !string.IsNullOrEmpty(value);
        }

        /// <summary>
        ///     Provide derived clases IsActiveChanged event.
        /// </summary>
        protected override void OnIsActiveChanged()
        {
            if (!IsActive)
                Value = null;
            base.OnIsActiveChanged();
        }
    }
}