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
    ///     Defines the contract for a string filter.
    /// </summary>
    public interface IStringFilter : IPropertyFilter
    {
        /// <summary>
        ///     Get or set string filter compare mode.
        /// </summary>
        StringFilterMode Mode { get; set; }

        /// <summary>
        ///     Gets or sets the value to look for.
        /// </summary>
        string Value { get; set; }
    }
}