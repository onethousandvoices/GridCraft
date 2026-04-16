using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Visuals", fileName = "ConstructionVisuals")]
    public sealed class ConstructionVisualsAsset : ScriptableObject
    {
        [SerializeField] private GridCellHighlightView _gridCellHighlightPrefab;
        [SerializeField] private Material _ghostValidMaterial;
        [SerializeField] private Material _ghostInvalidMaterial;
        [SerializeField] private Material _gridHighlightValidMaterial;
        [SerializeField] private Material _gridHighlightInvalidMaterial;
        [SerializeField] private GameObject _selectionModelPrefab;

        public GridCellHighlightView GridCellHighlightPrefab => _gridCellHighlightPrefab;

        public Material GhostValidMaterial => _ghostValidMaterial;

        public Material GhostInvalidMaterial => _ghostInvalidMaterial;

        public Material GridHighlightValidMaterial => _gridHighlightValidMaterial;

        public Material GridHighlightInvalidMaterial => _gridHighlightInvalidMaterial;

        public GameObject SelectionModelPrefab => _selectionModelPrefab;
    }
}