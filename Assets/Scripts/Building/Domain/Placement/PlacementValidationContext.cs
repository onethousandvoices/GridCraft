namespace GridCraft.Construction.Runtime
{
    public readonly record struct PlacementValidationContext(
        GridCell Origin,
        Footprint Footprint,
        int Cost);
}