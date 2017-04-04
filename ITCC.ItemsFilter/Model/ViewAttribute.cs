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
using System.Diagnostics;
using System.Windows;

namespace ITCC.ItemsFilter.Model
{
    /// <summary>
    ///     Specify the [ModelView] class that present model.
    /// </summary>
    public class ViewAttribute : Attribute
    {
        public ViewAttribute(Type ViewType = null)
        {
            Debug.Assert(ViewType == null || typeof (FrameworkElement).IsAssignableFrom(ViewType));
            this.ViewType = ViewType;
        }

        public Type ViewType { get; }
    }
}