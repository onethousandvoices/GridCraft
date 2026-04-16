using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class ConstructionUiInstaller : IConstructionInstallerModule
    {
        private readonly MainCanvasView _mainCanvasView;

        public ConstructionUiInstaller(ConstructionSceneContext sceneContext) => _mainCanvasView = sceneContext.MainCanvasView;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_mainCanvasView.BuildCatalogView);
            builder.RegisterInstance(_mainCanvasView.PlacementControlsView);
            builder.RegisterInstance(_mainCanvasView.ResourceBarView);
            builder.RegisterEntryPoint<ResourceController>();
            builder.RegisterEntryPoint<BuildCatalogController>();
            builder.RegisterEntryPoint<PlacementControlsController>();
        }
    }
}