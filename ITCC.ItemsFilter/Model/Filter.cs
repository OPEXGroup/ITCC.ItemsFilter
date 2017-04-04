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
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using ITCC.ItemsFilter.ViewModel;

namespace ITCC.ItemsFilter.Model
{
    /// <summary>
    ///     Base class for a filter.
    /// </summary>
    public abstract class Filter : IFilter, INotifyPropertyChanged
    {
        /// <summary>
        ///     Represent action that determine is item match filter.
        /// </summary>
        /// <param name="sender">FilterPresenter that contains filter.</param>
        /// <param name="e">FilterEventArgs include Item and Accepted fields.</param>
        public abstract void IsMatch(FilterPresenter sender, FilterEventArgs e);

        /// <summary>
        ///     Get or set value, determines is filter IsMatch action include in parentCollection filter.
        /// </summary>
        public bool IsActive
        {
            get { return isActive; }
            set
            {
                if (isActive != value)
                {
                    isActive = value;
                    var defer = FilterPresenter == null ? null : FilterPresenter.DeferRefresh();
                    OnIsActiveChanged();
                    if (defer != null)
                        defer.Dispose();
                    RaisePropertyChanged("IsActive");
                }
            }
        }

        private readonly List<FilterControlVm> attachedFilterControlVmodels = new List<FilterControlVm>();
        private bool isActive;
        private string name = "Filter:";

        /// <summary>
        ///     Get attached FilterPresenter.
        /// </summary>
        public FilterPresenter FilterPresenter { get; private set; }

        /// <summary>
        ///     Get or set Name of filter.
        /// </summary>
        public string Name
        {
            get { return name; }
            set
            {
                if (name != value)
                {
                    name = value;
                    RaisePropertyChanged("Name");
                }
            }
        }

        /// <summary>
        ///     Number attached to filter instances FilterControlVm.
        /// </summary>
        public int CountAttachedFilterControls
        {
            get { return attachedFilterControlVmodels.Count; }
        }

        /// <summary>
        ///     Provides class handling for the AttachPresenter event that occurs when FilterPresenter is attached.
        /// </summary>
        protected virtual void OnAttachPresenter(FilterPresenter presenter)
        {
        }

        /// <summary>
        ///     Provides class handling for the DetachPresenter event that occurs when FilterPresenter is detached.
        /// </summary>
        protected virtual void OnDetachPresenter(FilterPresenter presenter)
        {
        }

        /// <summary>
        ///     Provide derived class IsActiveChanged event.
        /// </summary>
        protected virtual void OnIsActiveChanged()
        {
            RaiseFilterChanged();
        }

        /// <summary>
        ///     Report attached listeners that filter changed.
        /// </summary>
        protected void RaiseFilterChanged()
        {
            if (FilterPresenter != null)
                FilterPresenter.ReceiveFilterChanged(this);
            foreach (var vm in attachedFilterControlVmodels)
            {
                vm.FilterChanged(this);
            }
        }

        internal void Attach(FilterControlVm vm)
        {
            if (!attachedFilterControlVmodels.Contains(vm))
                attachedFilterControlVmodels.Add(vm);
        }

        internal void Detach(FilterPresenter presenter)
        {
            if (presenter != null)
                presenter.Filter -= IsMatch;
            if (presenter == FilterPresenter)
                FilterPresenter = null;
            OnDetachPresenter(presenter);
        }

        internal void Attach(FilterPresenter presenter)
        {
            FilterPresenter = presenter;
            if (FilterPresenter != null)
                FilterPresenter.ReceiveFilterChanged(this);
            OnAttachPresenter(presenter);
        }

        internal void Detach(FilterControlVm vm)
        {
            attachedFilterControlVmodels.Remove(vm);
        }

        #region Члены INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void RaisePropertyChanged(string propertyName)
        {
            VerifyPropertyName(propertyName);

            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        [Conditional("DEBUG")]
        [DebuggerStepThrough]
        protected void VerifyPropertyName(string propertyName)
        {
            var myType = GetType();

#if NETFX_CORE
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetTypeInfo().GetDeclaredProperty(propertyName) == null)
            {
                throw new ArgumentException("Property not found", propertyName);
            }
#else
            if (!string.IsNullOrEmpty(propertyName)
                && myType.GetProperty(propertyName) == null)
            {
#if !SILVERLIGHT
                var descriptor = this as ICustomTypeDescriptor;

                if (descriptor != null)
                {
                    if (descriptor.GetProperties()
                        .Cast<PropertyDescriptor>()
                        .Any(property => property.Name == propertyName))
                    {
                        return;
                    }
                }
#endif

                throw new ArgumentException("Property not found", propertyName);
            }
#endif
        }

        #endregion
    }
}