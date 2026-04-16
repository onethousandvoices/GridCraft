using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class InputController : IStartable, ITickable, IDisposable
    {
        private readonly List<RaycastResult> _raycastResults = new(8);

        [Inject] private BuildModeStateMachine _buildModeStateMachine;
        [Inject] private IPlacementController _placementController;
        [Inject] private IPointerInputSource _pointerInputSource;

        private PointerEventData _pointerEventData;
        private EventSystem _eventSystem;

        public void Start() => _pointerInputSource.Enable();

        public void Tick()
        {
            if (!_buildModeStateMachine.AcceptsPlacementPointer) return;

            if (!_pointerInputSource.TryGetActivePointer(out var screenPosition, out _)) return;

            if (IsPointerOverUi(screenPosition)) return;

            _placementController.UpdatePlacementPointer(screenPosition);
        }

        private bool IsPointerOverUi(Vector2 screenPosition)
        {
            var eventSystem = EventSystem.current;
            if (!eventSystem) return false;

            if (_eventSystem != eventSystem || _pointerEventData is null)
            {
                _eventSystem = eventSystem;
                _pointerEventData = new(_eventSystem);
            }

            _pointerEventData.position = screenPosition;
            _raycastResults.Clear();
            _eventSystem.RaycastAll(_pointerEventData, _raycastResults);
            return _raycastResults.Count > 0;
        }

        public void Dispose() => _pointerInputSource.Disable();
    }
}