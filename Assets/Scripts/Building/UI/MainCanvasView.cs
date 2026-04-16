using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class MainCanvasView : MonoBehaviour
    {
        [SerializeField] private BuildCatalogView _buildCatalogView;
        [SerializeField] private PlacementControlsView _placementControlsView;
        [SerializeField] private ResourceBarView _resourceBarView;

        public BuildCatalogView BuildCatalogView => _buildCatalogView;

        public PlacementControlsView PlacementControlsView => _placementControlsView;

        public ResourceBarView ResourceBarView => _resourceBarView;

        private void OnValidate() => CacheReferences();

        private void CacheReferences()
        {
            CacheBuildCatalogView();
            CachePlacementControlsView();
            CacheResourceBarView();
        }

        private void CacheBuildCatalogView()
        {
            var views = GetComponentsInChildren<BuildCatalogView>(true);
            if (views.Length > 0) _buildCatalogView = views[0];
        }

        private void CachePlacementControlsView()
        {
            var views = GetComponentsInChildren<PlacementControlsView>(true);
            if (views.Length > 0) _placementControlsView = views[0];
        }

        private void CacheResourceBarView()
        {
            var views = GetComponentsInChildren<ResourceBarView>(true);
            if (views.Length > 0) _resourceBarView = views[0];
        }
    }
}