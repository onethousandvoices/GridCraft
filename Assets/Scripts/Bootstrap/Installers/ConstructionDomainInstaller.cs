using GridCraft.Construction.Runtime;
using VContainer;

namespace GridCraft.Construction
{
    public sealed class ConstructionDomainInstaller : IConstructionInstallerModule
    {
        private readonly ConstructionSceneContext _sceneContext;

        public ConstructionDomainInstaller(ConstructionSceneContext sceneContext) => _sceneContext = sceneContext;

        public void Install(IContainerBuilder builder)
        {
            builder.Register(_ => CreateGridService(), Lifetime.Singleton);
            builder.Register(_ => CreateGridMapModel(), Lifetime.Singleton);
            builder.Register(_ => CreateWalletService(), Lifetime.Singleton);
            builder.Register<FootprintRotationService>(Lifetime.Singleton);
            builder.Register<BuildModeStateMachine>(Lifetime.Singleton);
            builder.Register<BuildSessionModel>(Lifetime.Singleton);
        }

        private GridMapModel CreateGridMapModel()
            => new(
                _sceneContext.SettingsAsset.Layout.GridWidth,
                _sceneContext.SettingsAsset.Layout.GridHeight,
                _sceneContext.SettingsAsset.Layout.CreateObstacleCells(),
                _sceneContext.SettingsAsset.Layout.CreateForbiddenCells());

        private GridService CreateGridService()
            => new(
                _sceneContext.SettingsAsset.Layout.GridWidth,
                _sceneContext.SettingsAsset.Layout.GridHeight,
                _sceneContext.SettingsAsset.Layout.CellSize,
                _sceneContext.SettingsAsset.Layout.GridOrigin);

        private WalletService CreateWalletService() => new(_sceneContext.SettingsAsset.Economy.StartingResources);
    }
}