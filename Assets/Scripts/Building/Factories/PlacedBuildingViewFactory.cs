using VContainer;

namespace GridCraft.Construction
{
    public interface IPlacedBuildingViewFactory
    {
        PlacedBuildingView Rent(BuildingDefinitionAsset definition);

        void Return(PlacedBuildingView view);
    }

    public sealed class PlacedBuildingViewFactory : IPlacedBuildingViewFactory
    {
        private const int PREWARM_COUNT = 2;

        private readonly ComponentPool<BuildingDefinitionAsset, PlacedBuildingView> _pool;

        public PlacedBuildingViewFactory(
            ConstructionCatalogAsset constructionCatalogAsset,
            LocationRootView locationRootView,
            IObjectResolver resolver)
        {
            _pool = new(resolver, locationRootView.DynamicRoot);

            var buildings = constructionCatalogAsset.Buildings;
            var count = buildings.Length;
            for (var i = 0; i < count; i++)
            {
                var definition = buildings[i];
                _pool.Prewarm(definition, definition.PlacedPrefab, PREWARM_COUNT, view => view.Hide());
            }
        }

        public PlacedBuildingView Rent(BuildingDefinitionAsset definition)
        {
            var view = _pool.Rent(definition, definition.PlacedPrefab);
            view.Prepare();
            return view;
        }

        public void Return(PlacedBuildingView view) => _pool.Return(view);
    }
}