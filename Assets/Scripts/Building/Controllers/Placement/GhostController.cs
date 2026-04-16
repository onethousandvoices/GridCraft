using UnityEngine;
using VContainer;

namespace GridCraft.Construction
{
    public sealed class GhostController : IGhostController
    {
        private BuildingDefinitionAsset _activeDefinition;
        private PlacedBuildingView _activeGhost;

        [Inject] private ConstructionVisualsAsset _constructionVisualsAsset;
        [Inject] private IPlacedBuildingViewFactory _placedBuildingViewFactory;

        public void Hide()
        {
            if (!_activeGhost) return;

            _activeGhost.Hide();
            _placedBuildingViewFactory.Return(_activeGhost);
            _activeGhost = null;
            _activeDefinition = null;
        }

        public void Show(BuildingDefinitionAsset definition, Vector3 position, Quaternion rotation, bool canPlace)
        {
            if (!_activeGhost || _activeDefinition != definition)
            {
                Hide();
                _activeDefinition = definition;
                _activeGhost = _placedBuildingViewFactory.Rent(definition);
            }

            var material = canPlace ? _constructionVisualsAsset.GhostValidMaterial : _constructionVisualsAsset.GhostInvalidMaterial;
            _activeGhost.ShowGhost(position, rotation, material);
        }

        public bool TryCommit(out PlacedBuildingView view)
        {
            if (!_activeGhost)
            {
                view = null;
                return false;
            }

            view = _activeGhost;
            _activeGhost = null;
            _activeDefinition = null;
            return true;
        }
    }
}