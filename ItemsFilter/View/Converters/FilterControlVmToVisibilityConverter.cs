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
using System.Globalization;
using System.Windows;
using System.Windows.Data;
using BolapanControl.ItemsFilter.ViewModel;

namespace BolapanControl.ItemsFilter.View
{
    [ValueConversion(typeof (Visibility), typeof (FilterControlVm))]
    public class FilterControlVmToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is FilterControlVm)
            {
                var vm = (FilterControlVm) value;
                return vm.IsEnable ? Visibility.Visible : Visibility.Collapsed;
            }
            return
                Visibility.Collapsed;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}