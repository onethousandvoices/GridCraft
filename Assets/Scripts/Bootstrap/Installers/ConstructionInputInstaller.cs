using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class ConstructionInputInstaller : IConstructionInstallerModule
    {
        public void Install(IContainerBuilder builder)
        {
            builder.Register<ScreenTouchInputSource>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.Register<TouchWorldProjector>(Lifetime.Singleton).AsImplementedInterfaces();
            builder.RegisterEntryPoint<InputController>();
        }
    }
}