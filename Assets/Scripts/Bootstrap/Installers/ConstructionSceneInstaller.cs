using VContainer;

namespace GridCraft.Construction
{
    public sealed class ConstructionSceneInstaller : IConstructionInstallerModule
    {
        private readonly ConstructionSceneContext _sceneContext;

        public ConstructionSceneInstaller(ConstructionSceneContext sceneContext) => _sceneContext = sceneContext;

        public void Install(IContainerBuilder builder)
        {
            builder.RegisterInstance(_sceneContext.MainCamera);
            builder.RegisterInstance(_sceneContext.SettingsAsset.Catalog);
            builder.RegisterInstance(_sceneContext.SettingsAsset.Visuals);
            builder.RegisterInstance(_sceneContext.LocationRootView);
        }
    }
}