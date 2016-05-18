// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.View
{
    public interface IFilterView
    {
        IFilter Model { get; }
    }
}