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
using System.Collections.Generic;
using System.Collections.Specialized;

namespace BolapanControl.ItemsFilter.Model
{
    public class FiltersCollection
    {
        private readonly ListDictionary dictionary = new ListDictionary();
        private readonly FilterPresenter parent;

        internal FiltersCollection(FilterPresenter parent)
        {
            this.parent = parent;
        }

        internal Filter this[Type key]
        {
            get { return (Filter) dictionary[key]; }
            set
            {
                var defer = parent.DeferRefresh();
                Filter filter;
                if (dictionary.Contains(key))
                {
                    filter = (Filter) dictionary[key];
                    filter.Detach(parent);
                }
                dictionary[key] = filter = value;
                filter.Attach(parent);
                defer.Dispose();
            }
        }

        public IEnumerable<Filter> Filters
        {
            get
            {
                var enumerator = dictionary.Values.GetEnumerator();
                while (enumerator.MoveNext())
                    yield return (Filter) enumerator.Current;
            }
        }

        public int Count
        {
            get { return dictionary.Count; }
        }

        internal bool ContainsKey(Type filterKey)
        {
            return dictionary.Contains(filterKey);
        }

        internal void Remove(Type key)
        {
            if (dictionary.Contains(key))
            {
                var defer = parent.DeferRefresh();
                var filter = (Filter) dictionary[key];
                filter.Detach(parent);
                dictionary.Remove(key);
                defer.Dispose();
            }
        }

        internal void Remove(Filter filter)
        {
            Type key = null;
            var enumerator = dictionary.GetEnumerator();
            while (enumerator.MoveNext())
            {
                if (enumerator.Value == filter)
                {
                    key = (Type) enumerator.Key;
                    break;
                }
            }
            if (key != null)
                dictionary.Remove(key);
        }
    }
}