// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Diagnostics;
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.Initializer
{
    /// <summary>
    ///     Define ValueFilter initializer.
    /// </summary>
    public class ValueFilterInitializer : FilterInitializer
    {
        /// <summary>
        ///     Generate new instance of Filter class, if it is possible for filterPresenter and key.
        /// </summary>
        /// <param name="filterPresenter">FilterPresenter, which can be attached Filter</param>
        /// <param name="key">
        ///     Key for generated Filter. For PropertyFilter, key used as the name for binding property in
        ///     filterPresenter.Parent collection.
        /// </param>
        /// <returns>Instance of Filter class or null.</returns>
        public override Filter NewFilter(FilterPresenter filterPresenter, object key)
        {
            Debug.Assert(filterPresenter != null);
            Debug.Assert(key != null);
            var filter = new EqualFilter<object>(item => item, filterPresenter.CollectionView.SourceCollection);
            return filter;
        }
    }
}