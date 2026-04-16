namespace GridCraft.Construction.Runtime
{
    public sealed class FootprintRotationService
    {
        public static Footprint Rotate(Footprint footprint, int rotationQuarterTurns)
            => (rotationQuarterTurns & 1) == 0
                ? footprint
                : new(footprint.Height, footprint.Width);
    }
}