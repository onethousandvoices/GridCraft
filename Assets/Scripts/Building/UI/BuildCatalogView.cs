using System;
using System.Collections.Generic;
using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class BuildCatalogView : MonoBehaviour
    {
        [SerializeField] private BuildCardView _cardTemplate;
        [SerializeField] private BuildCardView[] _initialCards = Array.Empty<BuildCardView>();
        [SerializeField] private RectTransform _contentRoot;

        private readonly List<BuildCardView> _instances = new();

        public BuildCardView[] Cards { get; private set; } = Array.Empty<BuildCardView>();

        private void Awake() => Build();

        private void OnValidate() => CacheReferences();

        private void Build()
        {
            if (_instances.Count > 0) return;

            var count = _initialCards.Length;
            for (var i = 0; i < count; i++)
                _instances.Add(_initialCards[i]);
        }

        public void SetCatalog(BuildingDefinitionAsset[] definitions)
        {
            Build();

            var count = definitions.Length;
            EnsureCardCount(count);
            Cards = new BuildCardView[count];

            var cardCount = _instances.Count;
            for (var i = 0; i < cardCount; i++)
            {
                var isVisible = i < count;
                _instances[i].SetActiveSafe(isVisible);
                if (isVisible)
                {
                    _instances[i].Bind(definitions[i]);
                    Cards[i] = _instances[i];
                }
            }
        }

        private void CacheReferences()
        {
            CacheContentRoot();
            CacheInitialCards();
            if (!_cardTemplate && _initialCards.Length > 0) _cardTemplate = _initialCards[0];
        }

        private void CacheContentRoot()
        {
            var rectTransforms = GetComponentsInChildren<RectTransform>(true);
            var count = rectTransforms.Length;
            for (var i = 0; i < count; i++)
            {
                if (rectTransforms[i] == transform) continue;
                if (rectTransforms[i].name != "Content") continue;

                _contentRoot = rectTransforms[i];
                return;
            }
        }

        private void CacheInitialCards()
        {
            if (!_contentRoot) return;

            _initialCards = _contentRoot.GetComponentsInChildren<BuildCardView>(true);
            Array.Sort(_initialCards, CompareBySiblingIndex);
        }

        private BuildCardView CreateCard()
        {
            var card = Instantiate(_cardTemplate, _contentRoot);
            return card;
        }

        private static int CompareBySiblingIndex(BuildCardView left, BuildCardView right)
            => left.transform.GetSiblingIndex().CompareTo(right.transform.GetSiblingIndex());

        private void EnsureCardCount(int count)
        {
            while (_instances.Count < count)
                _instances.Add(CreateCard());
        }
    }
}