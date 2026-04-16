using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GridCraft.Construction
{
    public sealed class BuildCardView : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public event Action<BuildCardView, Vector2> DragStarted;
        public event Action<BuildCardView, Vector2> Dragged;
        public event Action<BuildCardView, Vector2> DragEnded;

        private static readonly Color AffordableColor = new(0.22f, 0.29f, 0.24f, 0.98f);
        private static readonly Color UnaffordableColor = new(0.29f, 0.19f, 0.18f, 0.98f);

        [SerializeField] private Image _background;
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _costLabel;
        [SerializeField] private TextMeshProUGUI _nameLabel;

        public BuildingDefinitionAsset Definition { get; private set; }

        private void OnValidate() => CacheReferences();

        public void Bind(BuildingDefinitionAsset definition)
        {
            Definition = definition;
            _icon.sprite = definition.Icon;
            _nameLabel.text = definition.DisplayName;
            _costLabel.text = definition.Cost.ToString();
            SetAffordable(true);
        }

        public void OnBeginDrag(PointerEventData eventData) => DragStarted?.Invoke(this, eventData.position);

        public void OnDrag(PointerEventData eventData) => Dragged?.Invoke(this, eventData.position);

        public void OnEndDrag(PointerEventData eventData) => DragEnded?.Invoke(this, eventData.position);

        public void SetAffordable(bool isAffordable) => _background.color = isAffordable ? AffordableColor : UnaffordableColor;

        private void CacheReferences()
        {
            if (!_background) _background = GetComponent<Image>();

            var images = GetComponentsInChildren<Image>(true);
            var imageCount = images.Length;
            for (var i = 0; i < imageCount; i++)
            {
                if (images[i] == _background) continue;

                if (images[i].name != "Icon") continue;
                _icon = images[i];
                break;
            }

            var labels = GetComponentsInChildren<TextMeshProUGUI>(true);
            var labelCount = labels.Length;
            for (var i = 0; i < labelCount; i++)
            {
                if (labels[i].name == "Name") _nameLabel = labels[i];
                if (labels[i].name == "Cost") _costLabel = labels[i];
            }
        }
    }
}