﻿// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Input;
using BolapanControl.ItemsFilter.Initializer;
using BolapanControl.ItemsFilter.ViewModel;

namespace BolapanControl.ItemsFilter
{
    /// <summary>
    ///     Represent BolapanControls.PropertyFilter.ColumnFilter control, which show filter View for associated Model.
    ///     If defined as part of System.Windows.Controls.Primitives.DataGridColumnHeader template, represent filter View for
    ///     current column.
    /// </summary>
    [TemplateVisualState(GroupName = "FilterState", Name = "Disable")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Enable")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Active")]
    [TemplateVisualState(GroupName = "FilterState", Name = "Open")]
    [TemplateVisualState(GroupName = "FilterState", Name = "OpenActive")]
    [TemplatePart(Name = PART_FiltersView, Type = typeof (Popup))]
    public class ColumnFilter : FilterControl
    {
        internal const string PART_FiltersView = "PART_FilterView";

        private Popup partFilterView;


        static ColumnFilter()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (ColumnFilter),
                new FrameworkPropertyMetadata(typeof (ColumnFilter)));
            CommandManager.RegisterClassCommandBinding(typeof (ColumnFilter),
                new CommandBinding(FilterCommand.Show, DoShowFilter, CanShowFilter));
        }

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes (such as a
        ///     rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            partFilterView = GetTemplateChild(PART_FiltersView) as Popup;
        }

        /// <summary>
        ///     Initializes the filter view model.
        /// </summary>
        protected override FilterControlVm CreateModel()
        {
            FilterControlVm vm = null;
            filterPresenter = Parent == null ? null : FilterPresenter.TryGet(ParentCollection);
            if (filterPresenter != null)
            {
                if (Key == null)
                {
                    var columnHeader = this.GetParent<DataGridColumnHeader>();
                    if (columnHeader == null)
                        return null;
                    var column = columnHeader.Column;
                    if (column == null)
                    {
                        return null;
                    }
                    var dataGrid = columnHeader.GetParent<DataGrid>();
                    if (dataGrid == null)
                    {
                        return null;
                    }
                    if (column.DisplayIndex >= dataGrid.Columns.Count)
                    {
                        return null;
                    }
                    IEnumerable<FilterInitializer> initializers = GetInitializers(column) ?? FilterInitializersManager;
                    var key = Key ?? GetColumnKey(column);
                    vm = filterPresenter.TryGetFilterControlVm(key, initializers);
                }
                else
                {
                    IEnumerable<FilterInitializer> initializers = FilterInitializersManager;
                    vm = filterPresenter.TryGetFilterControlVm(Key, initializers);
                }
                if (vm != null)
                    vm.IsEnable = true;
            }
            return vm;
        }

        //
        // Summary:
        //     Invoked when an unhandled System.Windows.UIElement.MouseLeftButtonDown routed
        //     event is raised on this element. Implement this method to add class handling
        //     for this event.
        //
        // Parameters:
        //   e:
        //     The System.Windows.Input.MouseButtonEventArgs that contains the event data.
        //     The event data reports that the left mouse button was pressed.
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            e.Handled = true;
            base.OnMouseLeftButtonDown(e);
        }

        private static void CanShowFilter(object sender, CanExecuteRoutedEventArgs e)
        {
            var filter = (ColumnFilter) sender;
            e.CanExecute = filter.Model != null && filter.Model.IsEnable && filter.partFilterView != null;
        }

        private static void DoShowFilter(object sender, ExecutedRoutedEventArgs e)
        {
            ((ColumnFilter) sender).Model.IsOpen = true;
        }

        private string GetColumnKey(DataGridColumn column)
        {
            var attachedBinding = GetBindingPath(column);
            if (attachedBinding != null)
            {
                return string.IsNullOrWhiteSpace(attachedBinding) ? null : attachedBinding;
                ;
            }

            string bindingPath = null;
            if (column is DataGridBoundColumn)
            {
                var columnBound = column as DataGridBoundColumn;
                var binding = columnBound.Binding as Binding;
                if (binding != null)
                {
                    bindingPath = binding.Path.Path;
                }
            }
            else if (column is DataGridTemplateColumn)
            {
                var templateColumn = column as DataGridTemplateColumn;
                var header = templateColumn.Header as string;
                if (header == null)
                {
                    return null;
                }
                bindingPath = header;
            }
            else if (column is DataGridComboBoxColumn)
            {
                var comboBoxColumn = column as DataGridComboBoxColumn;
                var binding = comboBoxColumn.SelectedItemBinding as Binding;
                bindingPath = binding == null ? null : binding.Path.Path;
            }

            if (bindingPath == null || string.IsNullOrEmpty(bindingPath))
            {
                return null;
            }
            if (bindingPath.Contains("."))
                return bindingPath;
            return bindingPath;
        }

        #region BindingPath

        /// <summary>
        ///     BindingPath Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty BindingPathProperty =
            DependencyProperty.RegisterAttached("BindingPath", typeof (string), typeof (ColumnFilter),
                new FrameworkPropertyMetadata((string) null));

        /// <summary>
        ///     Gets the BindingPath property. This dependency property
        ///     indicates path to the property in ParentCollection.
        /// </summary>
        public static string GetBindingPath(DependencyObject d)
        {
            return (string) d.GetValue(BindingPathProperty);
        }

        /// <summary>
        ///     Sets the BindingPath property. This dependency property
        ///     indicates path to the property in ParentCollection.
        /// </summary>
        public static void SetBindingPath(DependencyObject d, string value)
        {
            d.SetValue(BindingPathProperty, value);
        }

        #endregion

        #region Initializers

        /// <summary>
        ///     Initializers Attached Dependency Property
        /// </summary>
        public static readonly DependencyProperty InitializersProperty =
            DependencyProperty.RegisterAttached("Initializers", typeof (FilterInitializersManager),
                typeof (ColumnFilter),
                new FrameworkPropertyMetadata(null));

        /// <summary>
        ///     Gets the FilterInitializersManager  that used for generate ColumnFilter.Model.
        /// </summary>
        public static FilterInitializersManager GetInitializers(DependencyObject d)
        {
            return (FilterInitializersManager) d.GetValue(InitializersProperty);
        }

        /// <summary>
        ///     Sets the FilterInitializersManager that used for generate ColumnFilter.Model.
        /// </summary>
        public static void SetInitializers(DependencyObject d, bool value)
        {
            d.SetValue(InitializersProperty, value);
        }

        #endregion
    }
}