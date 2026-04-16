using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Catalog", fileName = "ConstructionCatalog")]
    public sealed class ConstructionCatalogAsset : ScriptableObject
    {
        [SerializeField] private BuildingDefinitionAsset[] _buildings;

        public BuildingDefinitionAsset[] Buildings => _buildings;
    }
}