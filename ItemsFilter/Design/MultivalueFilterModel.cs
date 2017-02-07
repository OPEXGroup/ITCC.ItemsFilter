// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Collections;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using BolapanControl.ItemsFilter.Model;
using BolapanControl.ItemsFilter.ViewModel;

namespace BolapanControl.ItemsFilter.Design
{
    internal class MultiValueFilterModel : IMultiValueFilter
    {
        public IEnumerable AvailableValues
        {
            get { return values; }
            set { ; }
        }

        public ReadOnlyObservableCollection<object> SelectedValues
        {
            get { return new ReadOnlyObservableCollection<object>(selectedValues); }
        }

        public void SelectedValuesChanged(object sender, SelectionChangedEventArgs e)
        {
            ;
        }

        public bool IsActive
        {
            get { return true; }
            set { ; }
        }

        public void IsMatch(FilterPresenter sender, FilterEventArgs e)
        {
            ;
        }

        private readonly ObservableCollection<object> selectedValues;

        private readonly string[] values;

        public MultiValueFilterModel()
        {
            values = new[]
            {
                "Item 1",
                "Item2"
            };
            selectedValues = new ObservableCollection<object>();
            selectedValues.Add(values[0]);
        }

        public string Name
        {
            get { return "Equality:"; }
            set { ; }
        }

        public void Attach(FilterPresenter presenter)
        {
            ;
        }

        public void Detach(FilterPresenter presenter)
        {
            ;
        }

        public void Attach(FilterControlVm vm)
        {
            ;
        }

        public void Detach(FilterControlVm vm)
        {
            ;
        }
    }
}