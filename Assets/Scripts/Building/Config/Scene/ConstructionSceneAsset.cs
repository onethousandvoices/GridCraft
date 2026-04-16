using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Scene", fileName = "ConstructionScene")]
    public sealed class ConstructionSceneAsset : ScriptableObject
    {
        [SerializeField] private LocationRootView _locationPrefab;
        [SerializeField] private MainCanvasView _mainCanvasPrefab;

        public LocationRootView LocationPrefab => _locationPrefab;

        public MainCanvasView MainCanvasPrefab => _mainCanvasPrefab;
    }
}