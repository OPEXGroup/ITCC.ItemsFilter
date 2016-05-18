// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.View
{
    /// <summary>
    ///     Defile View control for IRangeFilter model.
    /// </summary>
    [ModelView]
    [TemplatePart(Name = PART_From, Type = typeof (TextBox))]
    [TemplatePart(Name = PART_To, Type = typeof (TextBox))]
    public class RangeFilterView : FilterViewBase<IRangeFilter>
    {
        private const string PART_From = "PART_From";
        private const string PART_To = "PART_To";
        private TextBox txtBoxFrom, textBoxTo;

        static RangeFilterView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (RangeFilterView),
                new FrameworkPropertyMetadata(typeof (RangeFilterView)));
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RangeFilterView" /> class.
        /// </summary>
        public RangeFilterView()
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="RangeFilterView" /> class and accept model.
        /// </summary>
        /// <param name="model">IRangeFilter model</param>
        public RangeFilterView(object model)
        {
            Model = model as IRangeFilter;
        }

        /// <summary>
        ///     When overridden in a derived class, is invoked whenever application code or internal processes (such as a
        ///     rebuilding layout pass) call <see cref="M:System.Windows.Controls.Control.ApplyTemplate" />.
        /// </summary>
        public override void OnApplyTemplate()
        {
            if (txtBoxFrom != null)
            {
                txtBoxFrom.RemoveHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
                txtBoxFrom.RemoveHandler(LostFocusEvent, new RoutedEventHandler(TextBox_LostFocus));
            }
            if (textBoxTo != null)
            {
                textBoxTo.RemoveHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown));
                textBoxTo.RemoveHandler(LostFocusEvent, new RoutedEventHandler(TextBox_LostFocus));
            }
            base.OnApplyTemplate();
            textBoxTo = GetTemplateChild(PART_To) as TextBox;
            txtBoxFrom = GetTemplateChild(PART_From) as TextBox;
            if (txtBoxFrom != null)
            {
                txtBoxFrom.AddHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown), true);
                txtBoxFrom.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBox_LostFocus), true);
            }
            if (textBoxTo != null)
            {
                textBoxTo.AddHandler(KeyDownEvent, new KeyEventHandler(TextBox_KeyDown), true);
                textBoxTo.AddHandler(LostFocusEvent, new RoutedEventHandler(TextBox_LostFocus), true);
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UpdateTextBoxSource((TextBox) sender);
        }

        private void TextBox_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                {
                    UpdateTextBoxSource((TextBox) sender);
                    e.Handled = true;
                    return;
                }
            }
        }

        private static void UpdateTextBoxSource(TextBox tb)
        {
            tb.GetBindingExpression(TextBox.TextProperty).UpdateSource();
        }
    }
}