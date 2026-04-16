using VContainer;

namespace GridCraft.Construction
{
    public sealed class ConstructionInstaller
    {
        private readonly IConstructionInstallerModule[] _modules;

        public ConstructionInstaller(ConstructionSceneContext sceneContext) => _modules = new IConstructionInstallerModule[]
        {
            new ConstructionSceneInstaller(sceneContext),
            new ConstructionUiInstaller(sceneContext),
            new ConstructionDomainInstaller(sceneContext),
            new ConstructionPlacementInstaller(),
            new ConstructionInputInstaller()
        };

        public void Install(IContainerBuilder builder)
        {
            var count = _modules.Length;
            for (var i = 0; i < count; i++)
                _modules[i].Install(builder);
        }
    }
}