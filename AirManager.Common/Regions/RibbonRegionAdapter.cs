using System.ComponentModel.Composition;
using AirManager.Infrastructure.Behaviors;
using Fluent;
using Microsoft.Practices.Prism.Regions;

namespace AirManager.Infrastructure.Regions
{
    [Export]
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        [ImportingConstructor]
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory) : base(regionBehaviorFactory)
        {
        }

        protected override void Adapt(IRegion region, Ribbon regionTarget)
        {
        }

        protected override void AttachBehaviors(IRegion region, Ribbon regionTarget)
        {
            base.AttachBehaviors(region, regionTarget);

            if (!region.Behaviors.ContainsKey(RibbonRegionSyncBehavior.BehaviorKey))
            {
                var regionBehavior = new RibbonRegionSyncBehavior {HostControl = regionTarget};
                region.Behaviors.Add(RibbonRegionSyncBehavior.BehaviorKey, regionBehavior);
            }
        }

        protected override IRegion CreateRegion()
        {
            return new SingleActiveRegion();
        }
    }
}