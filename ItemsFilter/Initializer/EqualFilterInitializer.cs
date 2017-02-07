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
using System.Linq;
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.Initializer
{
    /// <summary>
    ///     Define EqualFilter initializer.
    /// </summary>
    public class EqualFilterInitializer : PropertyFilterInitializer
    {
        private const string _filterName = "Equality";

        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);

            var propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && !propertyType.IsEnum
                )
            {
                var filter = (PropertyFilter) Activator.CreateInstance(
                    typeof (EqualFilter<>).MakeGenericType(propertyInfo.PropertyType),
                    propertyInfo,
                    GetAvailableValuesQuery(filterPresenter, propertyInfo)
                    );
                return filter;
            }
            return null;
        }

        /// <summary>
        ///     Returns a query that returns the unique item property values in the ItemsSource collection..
        /// </summary>
        public static IEnumerable GetAvailableValuesQuery(FilterPresenter filterPresenter, ItemPropertyInfo propInfo)
        {
            var source = filterPresenter.CollectionView.SourceCollection;
            if (source == null)
                return new object[0];
            var propertyDescriptor = propInfo.Descriptor as PropertyDescriptor;
            var sourceQuery = source.OfType<object>().Select(item => propertyDescriptor.GetValue(item));
            var propType = propertyDescriptor.PropertyType;
            if (typeof (IComparable).IsAssignableFrom(propType))
            {
                sourceQuery = sourceQuery.OrderBy(item => item);
            }
            else
                sourceQuery = sourceQuery.OrderBy(item => item == null ? "" : item.ToString());
            sourceQuery = sourceQuery.Distinct();
            return sourceQuery;
        }
    }
}