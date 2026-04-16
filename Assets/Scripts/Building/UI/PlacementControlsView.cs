using System;
using UnityEngine;
using UnityEngine.UI;

namespace GridCraft.Construction
{
    public sealed class PlacementControlsView : MonoBehaviour
    {
        public event Action RotatePressed;
        public event Action ConfirmPressed;
        public event Action CancelPressed;

        [SerializeField] private Button _cancelButton;
        [SerializeField] private Button _confirmButton;
        [SerializeField] private Button _rotateButton;

        private void Awake() => Build();

        private void OnValidate() => CacheReferences();

        private void Build()
        {
            _rotateButton.onClick.RemoveListener(OnRotatePressed);
            _confirmButton.onClick.RemoveListener(OnConfirmPressed);
            _cancelButton.onClick.RemoveListener(OnCancelPressed);
            _rotateButton.onClick.AddListener(OnRotatePressed);
            _confirmButton.onClick.AddListener(OnConfirmPressed);
            _cancelButton.onClick.AddListener(OnCancelPressed);
            this.SetActiveSafe(false);
            _rotateButton.interactable = false;
            _confirmButton.interactable = false;
            _cancelButton.interactable = false;
        }

        public void SetState(bool isVisible, bool canRotate, bool canConfirm)
        {
            this.SetActiveSafe(isVisible);
            _rotateButton.interactable = canRotate;
            _confirmButton.interactable = canConfirm;
            _cancelButton.interactable = isVisible;
        }

        private void CacheReferences()
        {
            var buttons = GetComponentsInChildren<Button>(true);
            var count = buttons.Length;
            for (var i = 0; i < count; i++)
            {
                if (buttons[i].name == "Rotate") _rotateButton = buttons[i];
                if (buttons[i].name == "Confirm") _confirmButton = buttons[i];
                if (buttons[i].name == "Cancel") _cancelButton = buttons[i];
            }
        }

        private void OnCancelPressed() => CancelPressed?.Invoke();

        private void OnConfirmPressed() => ConfirmPressed?.Invoke();

        private void OnRotatePressed() => RotatePressed?.Invoke();
    }
}