using GridCraft.Construction.Runtime;

namespace GridCraft.Construction
{
    public interface IGridHighlightController
    {
        void Hide();

        void Show(PlacementPreview preview);
    }
}