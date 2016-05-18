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
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.Initializer
{
    /// <summary>
    ///     Define RangeFilter initializer.
    /// </summary>
    public class RangeFilterInitializer : PropertyFilterInitializer
    {
        private const string _filterName = "Between";

        #region IPropertyFilterInitializer Members

        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            var propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof (IComparable).IsAssignableFrom(propertyType)
                && propertyType != typeof (string)
                && propertyType != typeof (bool)
                && !propertyType.IsEnum
                )
            {
                return
                    (PropertyFilter)
                        Activator.CreateInstance(typeof (RangeFilter<>).MakeGenericType(propertyInfo.PropertyType),
                            propertyInfo);
            }
            return null;
        }

        #endregion
    }
}