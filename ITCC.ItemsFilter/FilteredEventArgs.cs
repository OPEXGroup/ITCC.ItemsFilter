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

namespace ITCC.ItemsFilter
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