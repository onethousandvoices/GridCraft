using UnityEngine;

namespace GridCraft.Construction
{
    public interface IGhostController
    {
        void Hide();

        void Show(BuildingDefinitionAsset definition, Vector3 position, Quaternion rotation, bool canPlace);

        bool TryCommit(out PlacedBuildingView view);
    }
}