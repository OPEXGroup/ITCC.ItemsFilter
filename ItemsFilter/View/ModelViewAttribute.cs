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

namespace BolapanControl.ItemsFilter.View
{
    /// <summary>
    ///     Indicate that class can be use as View, have property Model and constructor width single parameter Model.
    /// </summary>
    public class ModelViewAttribute : Attribute
    {
    }
}