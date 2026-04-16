using UnityEngine;

namespace GridCraft.Construction
{
    public interface IPlacementController
    {
        void BeginCatalogDrag(BuildingDefinitionAsset definition, Vector2 screenPosition);

        void Cancel();

        void ConfirmPlacement();

        void EndCatalogDrag(Vector2 screenPosition);

        void RotateClockwise();

        void UpdateCatalogDrag(Vector2 screenPosition);

        void UpdatePlacementPointer(Vector2 screenPosition);
    }
}