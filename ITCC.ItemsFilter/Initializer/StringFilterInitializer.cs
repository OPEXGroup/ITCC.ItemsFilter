// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.ComponentModel;
using System.Diagnostics;
using ITCC.ItemsFilter.Model;

namespace ITCC.ItemsFilter.Initializer
{
    /// <summary>
    ///     Represent initializer for StringFilter.
    /// </summary>
    public class StringFilterInitializer : PropertyFilterInitializer
    {
        /// <summary>
        ///     Create new instance of StringFilter, if it is possible for filterPresenter in current state and key.
        /// </summary>
        protected override PropertyFilter NewFilter(FilterPresenter filterPresenter, ItemPropertyInfo propertyInfo)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(propertyInfo != null);
            var propertyType = propertyInfo.PropertyType;
            if (filterPresenter.ItemProperties.Contains(propertyInfo)
                && typeof (string).IsAssignableFrom(propertyInfo.PropertyType)
                && !propertyType.IsEnum
                )
            {
                return new StringFilter(propertyInfo);
            }
            return null;
        }
    }
}