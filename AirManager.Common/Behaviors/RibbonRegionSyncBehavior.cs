using System;
using System.Collections.Specialized;
using System.Windows;
using System.Windows.Controls;
using Fluent;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Regions.Behaviors;

namespace AirManager.Infrastructure.Behaviors
{
    public class RibbonRegionSyncBehavior : RegionBehavior, IHostAwareRegionBehavior
    {
        public const string BehaviorKey = "RibbonRegionSyncBehavior";
        private Ribbon _hostRibbon;

        public DependencyObject HostControl
        {
            get { return _hostRibbon; }
            set
            {
                _hostRibbon = value as Ribbon;
                if (_hostRibbon == null)
                {
                    throw new InvalidOperationException("HostControl must be of type Ribbon.");
                }
            }
        }

        protected override void OnAttach()
        {
            if (_hostRibbon == null)
            {
                throw new InvalidOperationException("Host ribbon cannot be null.");
            }

            _hostRibbon.SelectedTabChanged += HostRibbonOnSelectedTabChanged;
            Region.ActiveViews.CollectionChanged += ActiveViewsOnCollectionChanged;
            Region.Views.CollectionChanged += ViewsOnCollectionChanged;
        }

        private void ViewsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (object newItem in notifyCollectionChangedEventArgs.NewItems)
                    {
                        _hostRibbon.Tabs.Add((RibbonTabItem) newItem);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (object oldItem in notifyCollectionChangedEventArgs.OldItems)
                    {
                        _hostRibbon.Tabs.Remove((RibbonTabItem) oldItem);
                    }
                    break;
            }
        }

        private void ActiveViewsOnCollectionChanged(object sender,
            NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            switch (notifyCollectionChangedEventArgs.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    _hostRibbon.SelectedTabItem = (RibbonTabItem) notifyCollectionChangedEventArgs.NewItems[0];
                    break;
                case NotifyCollectionChangedAction.Remove:
                    if (_hostRibbon.SelectedTabItem != null &&
                        notifyCollectionChangedEventArgs.OldItems.Contains(_hostRibbon.SelectedTabItem))
                    {
                        _hostRibbon.SelectedTabItem = null;
                    }
                    break;
            }
        }

        private void HostRibbonOnSelectedTabChanged(object sender, SelectionChangedEventArgs selectionChangedEventArgs)
        {
            if (_hostRibbon.SelectedTabItem != null)
            {
                Region.Activate(_hostRibbon.SelectedTabItem);
            }
        }
    }
}