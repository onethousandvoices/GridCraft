using System.Collections.Generic;

namespace GridCraft.Construction.Runtime
{
    public sealed class PlacementValidationService
    {
        private readonly IReadOnlyList<IPlacementRule> _placementRules;

        public PlacementValidationService(IReadOnlyList<IPlacementRule> placementRules) => _placementRules = placementRules;

        public PlacementPreview Validate(GridCell origin, Footprint footprint, int rotationQuarterTurns, int cost)
        {
            var rotatedFootprint = FootprintRotationService.Rotate(footprint, rotationQuarterTurns);
            var context = new PlacementValidationContext(origin, rotatedFootprint, cost);
            var blockers = PlacementBlockers.None;
            var count = _placementRules.Count;
            for (var i = 0; i < count; i++)
                blockers |= _placementRules[i].Evaluate(context);

            return new(origin, rotatedFootprint, rotationQuarterTurns, blockers);
        }
    }
}