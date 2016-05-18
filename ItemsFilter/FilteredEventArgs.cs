// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System;
using System.ComponentModel;

namespace BolapanControl.ItemsFilter
{
    /// <summary>
    ///     Provides data for the <see cref="Filtered" /> event
    /// </summary>
    public class FilteredEventArgs : EventArgs
    {
        internal FilteredEventArgs(ICollectionView cv)
        {
            CollectionView = cv;
        }

        /// <summary>
        ///     Filtered CollectionView.
        /// </summary>
        public ICollectionView CollectionView { get; }
    }
}