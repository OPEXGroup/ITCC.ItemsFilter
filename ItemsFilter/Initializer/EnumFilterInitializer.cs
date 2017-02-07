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
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.Initializer
{
    /// <summary>
    ///     Filter initializer for EnumFilter.
    /// </summary>
    public class EnumFilterInitializer : PropertyFilterInitializer
    {
        /// <summary>
        ///     Generate new instance of EnumFilter class, if it is possible for a pair of filterPresenter and propertyInfo.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">Key, used as the name for binding property in filterPresenter.Parent collection.</param>
        /// <returns>Instance of EnumFilter class or null.</returns>
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);
            var propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && propertyType.IsEnum
                )
            {
                return
                    (PropertyFilter)
                        Activator.CreateInstance(typeof (EnumFilter<>).MakeGenericType(propertyInfo.PropertyType),
                            propertyInfo);
            }
            return null;
        }
    }
}