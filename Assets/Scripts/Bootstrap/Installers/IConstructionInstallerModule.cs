using VContainer;

namespace GridCraft.Construction
{
    public interface IConstructionInstallerModule
    {
        void Install(IContainerBuilder builder);
    }
}