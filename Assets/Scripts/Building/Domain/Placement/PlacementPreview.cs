namespace GridCraft.Construction.Runtime
{
    public readonly record struct PlacementPreview(
        GridCell Origin,
        Footprint Footprint,
        int RotationQuarterTurns,
        PlacementBlockers Blockers)
    {
        public bool CanPlace => Blockers == PlacementBlockers.None;
    }
}