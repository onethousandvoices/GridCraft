using VContainer;

namespace GridCraft.Construction
{
    public interface IGridHighlightViewFactory
    {
        GridCellHighlightView Rent();

        void Return(GridCellHighlightView view);
    }

    public sealed class GridHighlightViewFactory : IGridHighlightViewFactory
    {
        private readonly ComponentPool<int, GridCellHighlightView> _pool;
        private readonly ConstructionVisualsAsset _constructionVisualsAsset;

        public GridHighlightViewFactory(
            ConstructionCatalogAsset constructionCatalogAsset,
            ConstructionVisualsAsset constructionVisualsAsset,
            LocationRootView locationRootView,
            IObjectResolver resolver)
        {
            _constructionVisualsAsset = constructionVisualsAsset;
            _pool = new(resolver, locationRootView.HighlightRoot);
            _pool.Prewarm(0, _constructionVisualsAsset.GridCellHighlightPrefab, GetMaxHighlightCount(constructionCatalogAsset), Setup);
        }

        public GridCellHighlightView Rent() => _pool.Rent(0, _constructionVisualsAsset.GridCellHighlightPrefab);

        public void Return(GridCellHighlightView view) => _pool.Return(view);

        private static int GetMaxHighlightCount(ConstructionCatalogAsset constructionCatalogAsset)
        {
            var maxHighlightCount = 0;
            var buildings = constructionCatalogAsset.Buildings;
            var count = buildings.Length;
            for (var i = 0; i < count; i++)
            {
                var highlightCount = buildings[i].Footprint.Area;
                if (highlightCount > maxHighlightCount) maxHighlightCount = highlightCount;
            }

            return maxHighlightCount;
        }

        private void Setup(GridCellHighlightView view)
        {
            view.Prepare(_constructionVisualsAsset.SelectionModelPrefab);
            view.Hide();
        }
    }
}