// ****************************************************************************
// <author>mishkin Ivan</author>
// <email>Mishkin_Ivan@mail.ru</email>
// <date>28.01.2015</date>
// <project>ItemsFilter</project>
// <license> GNU General Public License version 3 (GPLv3) </license>
// ****************************************************************************

using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using BolapanControl.ItemsFilter.Model;

namespace BolapanControl.ItemsFilter.ViewModel
{
    public delegate void FilterControlStateChangedEventHandler(FilterControlVm sender, FilterControl.State newValue);

    /// <summary>
    ///     View model for <c>FilterControl</c>.
    ///     FilterControlVm is IEnumerable&lt;Filter&gt;.
    ///     An instance of FilterControlVm not created directly. Instead, the procedure
    ///     FilterPresenter.TryGetFilterControlVm(string viewKey, IEnumerable&lt;FilterInitializer&gt; filterInitializers)
    ///     prepares a model for FilterControl, as IEnumerable&lt;Filter&gt;, where each Filter instance bound to
    ///     FilterControlVm for the transmission of changes.
    ///     Usually, instance of FilterControlVm created by FilterControl when it need this and disposed on raise Unload event.
    /// </summary>
    public class FilterControlVm : ObservableCollection<Filter>, IDisposable
    {
        /// <summary>
        ///     Detach FilterControlVm from all filters and remove all filters from collection.
        /// </summary>
        public void Dispose()
        {
            lock (lockFlag)
            {
                if (!IsDisposed)
                {
                    IsDisposed = true;
                    Clear();
                }
            }
        }

        private readonly object lockFlag = new object();
        private readonly object stateLock = new object();
        private bool isActive;
        private bool isEnable;
        private bool isOpen;
        private FilterControl.State state;

        internal FilterControlVm()
        {
        }

        /// <summary>
        ///     Get FilterControl state.
        /// </summary>
        public FilterControl.State State
        {
            get { return state; }
            private set
            {
                if (state != value)
                {
                    state = value;
                    var stateChangedEvent = StateChanged;
                    if (stateChangedEvent != null)
                    {
                        lock (stateLock)
                        {
                            stateChangedEvent(this, state);
                        }
                    }
                    ;
                    OnPropertyChanged(new PropertyChangedEventArgs("State"));
                }
            }
        }

        /// <summary>
        ///     Get or set FilterControl.IsEnabled.
        /// </summary>
        public bool IsEnable
        {
            get { return isEnable; }
            set
            {
                if (isEnable != value)
                {
                    isEnable = value;
                    if (value)
                        State |= FilterControl.State.Enable;
                    else
                        State &= ~FilterControl.State.Enable;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsEnable"));
                }
            }
        }

        /// <summary>
        ///     Gets or sets whether the popup is currently displaying on the screen.
        /// </summary>
        public bool IsOpen
        {
            get { return isOpen; }
            set
            {
                if (isOpen != value)
                {
                    isOpen = value;
                    if (value)
                        State |= FilterControl.State.Open;
                    else
                        State &= ~FilterControl.State.Open;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
                }
            }
        }

        /// <summary>
        ///     Get or set whether filter is active.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    if (value)
                        State |= FilterControl.State.Active;
                    else
                        State &= ~FilterControl.State.Active;
                    OnPropertyChanged(new PropertyChangedEventArgs("IsActive"));
                }
            }
        }

        /// <summary>
        ///     Gets whether Dispose has been called.
        /// </summary>
        public bool IsDisposed { get; private set; }

        /// <summary>
        ///     Provede FilterControlStateChanged event.
        /// </summary>
        public event FilterControlStateChangedEventHandler StateChanged;

        //
        // Summary:
        //     Inserts a filter into the collection at the specified index and attach filter to FilterControlVm.
        //
        // Parameters:
        //   index:
        //     The zero-based index at which item should be inserted.
        //
        //   item:
        //     The object to insert.
        protected override void InsertItem(int index, Filter filter)
        {
            if (!IsDisposed)
            {
                base.InsertItem(index, filter);
                filter.Attach(this);
                FilterChanged(filter);
            }
        }

        //
        // Summary:
        //    Detach FilterControlVm from all filters and remove all filters from collection.
        protected override void ClearItems()
        {
            foreach (var item in Items)
            {
                item.Detach(this);
            }
            FilterChanged(null);
            base.ClearItems();
        }

        //
        // Summary:
        //     Replaces the filter at the specified index. 
        //     Detach FilterControlVm from removed filter and attach FilterControlVm to new filter.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to replace.
        //
        //   item:
        //     The new value for the element at the specified index.
        protected override void SetItem(int index, Filter filter)
        {
            if (!IsDisposed)
            {
                base[index].Detach(this);
                base.SetItem(index, filter);
                filter.Attach(this);
                FilterChanged(filter);
            }
        }

        //
        // Summary:
        //     Moves the filter at the specified index to a new location in the collection.
        //
        // Parameters:
        //   oldIndex:
        //     The zero-based index specifying the location of the item to be moved.
        //
        //   newIndex:
        //     The zero-based index specifying the new location of the item.
        protected override void MoveItem(int oldIndex, int newIndex)
        {
            if (!IsDisposed)
            {
                base.MoveItem(oldIndex, newIndex);
            }
        }

        //
        // Summary:
        //     Detach FilterControlVm from filter at the specified index of the collection and removes filter.
        //
        // Parameters:
        //   index:
        //     The zero-based index of the element to remove.
        protected override void RemoveItem(int index)
        {
            if (!IsDisposed)
            {
                base[index].Detach(this);
                base.RemoveItem(index);
                FilterChanged(null);
            }
        }

        internal void FilterChanged(Filter filter)
        {
            var active = false;
            foreach (var item in Items)
            {
                active |= item.IsActive;
            }
            IsActive = active;
        }
    }
}