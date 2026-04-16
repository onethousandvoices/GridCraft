using GridCraft.Construction.Runtime;
using VContainer;

namespace GridCraft.Construction
{
    public sealed class ConstructionPlacementInstaller : IConstructionInstallerModule
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<OutsideBoundsPlacementRule>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<OccupiedPlacementRule>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ObstaclePlacementRule>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ForbiddenPlacementRule>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<ResourcesPlacementRule>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlacementValidationService>(Lifetime.Singleton);
            builder.Register<PlacedBuildingViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GridHighlightViewFactory>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GhostController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<GridHighlightController>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<PlacementController>(Lifetime.Singleton).AsImplementedInterfaces();
        }
    }
}