// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Windows;
using System.Windows.Controls;

namespace BolapanControl.ItemsFilter
{
    /// <summary>
    ///     Define a standard DataGrid with the included ColumnFilter in the column header template.
    /// </summary>
    public class FilterDataGrid : DataGrid
    {
        static FilterDataGrid()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (FilterDataGrid),
                new FrameworkPropertyMetadata(typeof (FilterDataGrid)));
        }
    }
}