using System;
using GridCraft.Construction.Runtime;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class BuildCatalogController : IStartable, IDisposable
    {
        [Inject] private ConstructionCatalogAsset _constructionCatalogAsset;
        [Inject] private BuildCatalogView _buildCatalogView;
        [Inject] private IPlacementController _placementController;
        [Inject] private WalletService _walletService;

        private BuildCardView[] _cards = Array.Empty<BuildCardView>();

        public void Start()
        {
            _buildCatalogView.SetCatalog(_constructionCatalogAsset.Buildings);
            _cards = _buildCatalogView.Cards;

            var count = _cards.Length;
            for (var i = 0; i < count; i++)
            {
                _cards[i].DragStarted += OnDragStarted;
                _cards[i].Dragged += OnDragged;
                _cards[i].DragEnded += OnDragEnded;
            }

            _walletService.Changed += RefreshAffordability;
            RefreshAffordability();
        }

        private void OnDragEnded(BuildCardView cardView, Vector2 screenPosition)
            => _placementController.EndCatalogDrag(screenPosition);

        private void OnDragged(BuildCardView cardView, Vector2 screenPosition)
            => _placementController.UpdateCatalogDrag(screenPosition);

        private void OnDragStarted(BuildCardView cardView, Vector2 screenPosition)
            => _placementController.BeginCatalogDrag(cardView.Definition, screenPosition);

        private void RefreshAffordability()
        {
            var count = _cards.Length;
            for (var i = 0; i < count; i++)
                _cards[i].SetAffordable(_walletService.CanSpend(_cards[i].Definition.Cost));
        }

        public void Dispose()
        {
            _walletService.Changed -= RefreshAffordability;

            var count = _cards.Length;
            for (var i = 0; i < count; i++)
            {
                _cards[i].DragStarted -= OnDragStarted;
                _cards[i].Dragged -= OnDragged;
                _cards[i].DragEnded -= OnDragEnded;
            }
        }
    }
}