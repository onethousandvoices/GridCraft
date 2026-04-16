using GridCraft.Construction.Runtime;
using UnityEngine;
using VContainer;

namespace GridCraft.Construction
{
    public sealed class PlacementController : IPlacementController
    {
        private static readonly Vector3 PlacementOffset = new(0f, 0.02f, 0f);

        [Inject] private BuildModeStateMachine _buildModeStateMachine;
        [Inject] private BuildSessionModel _buildSessionModel;
        [Inject] private IGhostController _ghostController;
        [Inject] private IGridHighlightController _gridHighlightController;
        [Inject] private IPlacedBuildingViewFactory _placedBuildingViewFactory;
        [Inject] private GridMapModel _gridMapModel;
        [Inject] private GridService _gridService;
        [Inject] private PlacementValidationService _placementValidationService;
        [Inject] private IScreenWorldProjector _screenWorldProjector;
        [Inject] private WalletService _walletService;

        public void BeginCatalogDrag(BuildingDefinitionAsset definition, Vector2 screenPosition)
        {
            _buildSessionModel.Begin(definition, screenPosition);
            _buildModeStateMachine.EnterCatalogDrag();
            RefreshPreview(screenPosition);
        }

        public void Cancel()
        {
            _ghostController.Hide();
            _gridHighlightController.Hide();
            _buildModeStateMachine.Cancel();
            _buildSessionModel.Cancel();
        }

        public void ConfirmPlacement()
        {
            if (!_buildModeStateMachine.IsPlacementActive || !_buildSessionModel.HasPreview) return;

            var preview = _buildSessionModel.CurrentPreview;
            if (!preview.CanPlace) return;

            var definition = _buildSessionModel.CurrentDefinition;
            if (!_walletService.TrySpend(definition.Cost))
            {
                RefreshPreview(_buildSessionModel.LastScreenPosition);
                return;
            }

            if (!_ghostController.TryCommit(out var placedView))
                placedView = _placedBuildingViewFactory.Rent(definition);

            var position = _gridService.GetPlacementPosition(preview.Origin, preview.Footprint) + PlacementOffset;
            var rotation = Quaternion.Euler(0f, preview.RotationQuarterTurns * 90f, 0f);
            placedView.ShowPlaced(position, rotation);

            _gridMapModel.SetOccupied(preview.Origin, preview.Footprint, true);
            _gridHighlightController.Hide();
            _buildModeStateMachine.Cancel();
            _buildSessionModel.Cancel();
        }

        public void EndCatalogDrag(Vector2 screenPosition)
        {
            var hasPreview = RefreshPreview(screenPosition);
            if (hasPreview || _buildSessionModel.HasPreview)
            {
                _buildModeStateMachine.EnterPlacement();
                return;
            }

            Cancel();
        }

        public void RotateClockwise()
        {
            if (!_buildSessionModel.HasDefinition) return;

            _buildSessionModel.RotateClockwise();
            RefreshPreview(_buildSessionModel.LastScreenPosition);
        }

        public void UpdateCatalogDrag(Vector2 screenPosition) => RefreshPreview(screenPosition);

        public void UpdatePlacementPointer(Vector2 screenPosition)
        {
            if (!_buildModeStateMachine.AcceptsPlacementPointer) return;

            RefreshPreview(screenPosition);
        }

        private bool RefreshPreview(Vector2 screenPosition)
        {
            _buildSessionModel.SetLastScreenPosition(screenPosition);
            if (!_buildSessionModel.HasDefinition) return false;

            if (!_screenWorldProjector.TryProject(screenPosition, out var worldPosition) || !_gridService.TryGetCell(worldPosition, out var cell))
            {
                _buildSessionModel.ClearPreview();
                _ghostController.Hide();
                _gridHighlightController.Hide();
                return false;
            }

            var definition = _buildSessionModel.CurrentDefinition;
            var preview = _placementValidationService.Validate(cell, definition.Footprint, _buildSessionModel.RotationQuarterTurns, definition.Cost);
            _buildSessionModel.SetPreview(preview);

            var position = _gridService.GetPlacementPosition(preview.Origin, preview.Footprint) + PlacementOffset;
            var rotation = Quaternion.Euler(0f, preview.RotationQuarterTurns * 90f, 0f);
            _ghostController.Show(definition, position, rotation, preview.CanPlace);
            _gridHighlightController.Show(preview);

            return true;
        }
    }
}