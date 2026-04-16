namespace GridCraft.Construction.Runtime
{
    public sealed class OutsideBoundsPlacementRule : IPlacementRule
    {
        private readonly GridService _gridService;

        public OutsideBoundsPlacementRule(GridService gridService) => _gridService = gridService;

        public PlacementBlockers Evaluate(PlacementValidationContext context)
        {
            for (var y = 0; y < context.Footprint.Height; y++)
            for (var x = 0; x < context.Footprint.Width; x++)
            {
                GridCell cell = new(context.Origin.X + x, context.Origin.Y + y);
                if (!_gridService.Contains(cell)) return PlacementBlockers.OutsideBounds;
            }

            return PlacementBlockers.None;
        }
    }

    public sealed class OccupiedPlacementRule : IPlacementRule
    {
        private readonly GridMapModel _gridMapModel;
        private readonly GridService _gridService;

        public OccupiedPlacementRule(GridMapModel gridMapModel, GridService gridService)
        {
            _gridMapModel = gridMapModel;
            _gridService = gridService;
        }

        public PlacementBlockers Evaluate(PlacementValidationContext context)
        {
            for (var y = 0; y < context.Footprint.Height; y++)
            for (var x = 0; x < context.Footprint.Width; x++)
            {
                GridCell cell = new(context.Origin.X + x, context.Origin.Y + y);
                if (!_gridService.Contains(cell)) continue;
                if (_gridMapModel.IsOccupied(cell)) return PlacementBlockers.Occupied;
            }

            return PlacementBlockers.None;
        }
    }

    public sealed class ObstaclePlacementRule : IPlacementRule
    {
        private readonly GridMapModel _gridMapModel;
        private readonly GridService _gridService;

        public ObstaclePlacementRule(GridMapModel gridMapModel, GridService gridService)
        {
            _gridMapModel = gridMapModel;
            _gridService = gridService;
        }

        public PlacementBlockers Evaluate(PlacementValidationContext context)
        {
            for (var y = 0; y < context.Footprint.Height; y++)
            for (var x = 0; x < context.Footprint.Width; x++)
            {
                GridCell cell = new(context.Origin.X + x, context.Origin.Y + y);
                if (!_gridService.Contains(cell)) continue;
                if (_gridMapModel.IsObstacle(cell)) return PlacementBlockers.Obstacle;
            }

            return PlacementBlockers.None;
        }
    }

    public sealed class ForbiddenPlacementRule : IPlacementRule
    {
        private readonly GridMapModel _gridMapModel;
        private readonly GridService _gridService;

        public ForbiddenPlacementRule(GridMapModel gridMapModel, GridService gridService)
        {
            _gridMapModel = gridMapModel;
            _gridService = gridService;
        }

        public PlacementBlockers Evaluate(PlacementValidationContext context)
        {
            for (var y = 0; y < context.Footprint.Height; y++)
            for (var x = 0; x < context.Footprint.Width; x++)
            {
                GridCell cell = new(context.Origin.X + x, context.Origin.Y + y);
                if (!_gridService.Contains(cell)) continue;
                if (_gridMapModel.IsForbidden(cell)) return PlacementBlockers.Forbidden;
            }

            return PlacementBlockers.None;
        }
    }

    public sealed class ResourcesPlacementRule : IPlacementRule
    {
        private readonly WalletService _walletService;

        public ResourcesPlacementRule(WalletService walletService) => _walletService = walletService;

        public PlacementBlockers Evaluate(PlacementValidationContext context)
            => _walletService.CanSpend(context.Cost) ? PlacementBlockers.None : PlacementBlockers.InsufficientResources;
    }
}