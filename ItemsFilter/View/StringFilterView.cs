// This is an open source non-commercial project. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++ and C#: http://www.viva64.com
// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls.Primitives;
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.View
{
    /// <summary>
    ///     Defile View control for IStringFilter model.
    /// </summary>
    [ModelView]
    [TemplatePart(Name = PART_FilterType, Type = typeof (Selector))]
    public class StringFilterView : FilterViewBase<IStringFilter>, IFilterView
    {
        internal const string PART_FilterType = "PART_FilterType";

        /// <summary>
        ///     Instance of a selector allowing to choose the filtering mode
        /// </summary>
        private Selector _selectorFilterType;

        static StringFilterView()
        {
            DefaultStyleKeyProperty.OverrideMetadata(typeof (StringFilterView),
                new FrameworkPropertyMetadata(typeof (StringFilterView)));
        }

        /// <summary>
        ///     Create new instance of StringFilterView.
        /// </summary>
        public StringFilterView()
        {
        }

        /// <summary>
        ///     Create new instance of StringFilterView and accept IStringFilter model.
        /// </summary>
        /// <param name="model"></param>
        public StringFilterView(object model)
        {
            Model = model as IStringFilter;
        }

        /// <summary>
        ///     Called when the control template is applied to this control
        /// </summary>
        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            _selectorFilterType = GetTemplateChild(PART_FilterType) as Selector;
            if (_selectorFilterType != null)
            {
                _selectorFilterType.ItemsSource = GetFilterModes();
            }
        }

        private IEnumerable<StringFilterMode> GetFilterModes()
        {
            foreach (var item in typeof (StringFilterMode).GetEnumValues())
            {
                yield return (StringFilterMode) item;
            }
        }
    }
}