using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Settings", fileName = "ConstructionSettings")]
    public sealed class ConstructionSettingsAsset : ScriptableObject
    {
        [SerializeField] private ConstructionCatalogAsset _catalog;
        [SerializeField] private ConstructionEconomyAsset _economy;
        [SerializeField] private ConstructionLayoutAsset _layout;
        [SerializeField] private ConstructionSceneAsset _scene;
        [SerializeField] private ConstructionVisualsAsset _visuals;

        public ConstructionCatalogAsset Catalog => _catalog;

        public ConstructionEconomyAsset Economy => _economy;

        public ConstructionLayoutAsset Layout => _layout;

        public ConstructionSceneAsset Scene => _scene;

        public ConstructionVisualsAsset Visuals => _visuals;
    }
}