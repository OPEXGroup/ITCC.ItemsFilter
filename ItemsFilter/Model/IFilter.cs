// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

namespace BolapanControl.ItemsFilter.Model
{
    /// <summary>
    ///     Defines the contract for a filter used by the FilteredCollection
    /// </summary>
    public interface IFilter
    {
        /// <summary>
        ///     Get or set value that indicates are filter include in filter function.
        /// </summary>
        bool IsActive { get; set; }

        /// <summary>
        ///     Determines whether the specified target is matching the criteria.
        /// </summary>
        void IsMatch(FilterPresenter sender, FilterEventArgs e);
    }
}