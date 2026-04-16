using GridCraft.Construction.Runtime;
using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Building Definition", fileName = "BuildingDefinition")]
    public sealed class BuildingDefinitionAsset : ScriptableObject
    {
        [SerializeField] private string _displayName = "Building";
        [SerializeField] private int _cost = 100;
        [SerializeField] private Vector2Int _footprint = Vector2Int.one;
        [SerializeField] private Sprite _icon;
        [SerializeField] private PlacedBuildingView _placedPrefab;

        public string DisplayName => _displayName;

        public int Cost => _cost;

        public Footprint Footprint => new(_footprint.x, _footprint.y);

        public Sprite Icon => _icon;

        public PlacedBuildingView PlacedPrefab => _placedPrefab;

        private void OnValidate()
        {
            if (_cost < 0) _cost = 0;
            if (_footprint.x < 1) _footprint.x = 1;
            if (_footprint.y < 1) _footprint.y = 1;
        }
    }
}