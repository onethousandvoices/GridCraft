namespace GridCraft.Construction.Runtime
{
    public interface IPlacementRule
    {
        PlacementBlockers Evaluate(PlacementValidationContext context);
    }
}