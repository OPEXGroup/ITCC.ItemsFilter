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
using System.Collections;
using System.ComponentModel;
using System.Diagnostics;

namespace ITCC.ItemsFilter.Model
{
    /// <summary>
    ///     Define the logic for Enum values filter.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class EnumFilter<T> : EqualFilter<T>, IMultiValueFilter
    {
        /// <summary>
        ///     TryGet list of defined enum values.
        ///     Throw <exception cref="NotImplementedException" /> if attempt to set value.
        /// </summary>
        public override IEnumerable AvailableValues
        {
            get
            {
                if (allValues == null)
                {
                    var enumType = typeof (T);
                    if (enumType.IsEnum)
                    {
                        allValues = enumType.GetEnumValues();
                    }
                    else
                        allValues = new string[0];
                }
                return allValues;
            }
            set { throw new NotImplementedException(); }
        }

        public override void IsMatch(FilterPresenter sender, FilterEventArgs e)
        {
            if (e.Accepted)
            {
                if (e.Item == null)
                    e.Accepted = false;
                else
                {
                    var value = getter(e.Item);
                    e.Accepted = SelectedValues.Contains(value);
                }
            }
        }

        private Array allValues;

        /// <summary>
        ///     Create new instance of EnumFilter.
        /// </summary>
        /// <param name="propertyInfo">propertyInfo, used to access a property of the collection item</param>
        public EnumFilter(ItemPropertyInfo propertyInfo)
            : base(propertyInfo)
        {
            Debug.Assert(propertyInfo.PropertyType == typeof (T),
                "Invalid property type, the return type is not matching the class generic type.");
        }
    }
}