using System;
using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class PlacementControlsController : IStartable, IDisposable
    {
        [Inject] private BuildModeStateMachine _buildModeStateMachine;
        [Inject] private BuildSessionModel _buildSessionModel;
        [Inject] private IPlacementController _placementController;
        [Inject] private PlacementControlsView _placementControlsView;

        public void Start()
        {
            _placementControlsView.RotatePressed += OnRotatePressed;
            _placementControlsView.ConfirmPressed += OnConfirmPressed;
            _placementControlsView.CancelPressed += OnCancelPressed;

            _buildModeStateMachine.Changed += RefreshState;
            _buildSessionModel.Changed += RefreshState;
            RefreshState();
        }

        private void OnCancelPressed() => _placementController.Cancel();

        private void OnConfirmPressed() => _placementController.ConfirmPlacement();

        private void OnRotatePressed() => _placementController.RotateClockwise();

        private void RefreshState()
        {
            var isVisible = _buildModeStateMachine.ShowsPlacementControls;
            var canRotate = _buildModeStateMachine.AllowsPlacementRotation;
            var canConfirm = _buildModeStateMachine.IsPlacementActive && _buildSessionModel.HasPreview && _buildSessionModel.CurrentPreview.CanPlace;
            _placementControlsView.SetState(isVisible, canRotate, canConfirm);
        }

        public void Dispose()
        {
            _buildModeStateMachine.Changed -= RefreshState;
            _buildSessionModel.Changed -= RefreshState;

            _placementControlsView.RotatePressed -= OnRotatePressed;
            _placementControlsView.ConfirmPressed -= OnConfirmPressed;
            _placementControlsView.CancelPressed -= OnCancelPressed;
        }
    }
}