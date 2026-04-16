using System.Collections.Generic;
using GridCraft.Construction.Runtime;
using UnityEngine;
using VContainer;

namespace GridCraft.Construction
{
    public sealed class GridHighlightController : IGridHighlightController
    {
        private static readonly Vector3 HighlightPositionOffset = new(0f, 0.02f, 0f);

        private readonly List<GridCellHighlightView> _activeHighlights = new();
        private int _visibleCount;

        [Inject] private ConstructionVisualsAsset _constructionVisualsAsset;
        [Inject] private IGridHighlightViewFactory _gridHighlightViewFactory;
        [Inject] private GridService _gridService;

        public void Hide()
        {
            var count = _activeHighlights.Count;
            for (var i = 0; i < count; i++)
            {
                _activeHighlights[i].Hide();
                _gridHighlightViewFactory.Return(_activeHighlights[i]);
            }

            _activeHighlights.Clear();
            _visibleCount = 0;
        }

        public void Show(PlacementPreview preview)
        {
            var requiredCount = preview.Footprint.Area;
            while (_activeHighlights.Count < requiredCount)
                _activeHighlights.Add(_gridHighlightViewFactory.Rent());

            var material = preview.CanPlace ? _constructionVisualsAsset.GridHighlightValidMaterial : _constructionVisualsAsset.GridHighlightInvalidMaterial;
            var visibleCount = 0;

            for (var y = 0; y < preview.Footprint.Height; y++)
            for (var x = 0; x < preview.Footprint.Width; x++)
            {
                GridCell cell = new(preview.Origin.X + x, preview.Origin.Y + y);
                if (!_gridService.Contains(cell)) continue;

                var position = _gridService.GetCellCenter(cell) + HighlightPositionOffset;
                _activeHighlights[visibleCount].Show(position, material);
                visibleCount++;
            }

            for (var i = visibleCount; i < _visibleCount; i++)
                _activeHighlights[i].Hide();

            _visibleCount = visibleCount;
        }
    }
}