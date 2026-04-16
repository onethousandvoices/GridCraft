using System;

namespace GridCraft.Construction.Runtime
{
    [Flags]
    public enum PlacementBlockers : byte
    {
        None = 0,
        OutsideBounds = 1 << 0,
        Occupied = 1 << 1,
        Obstacle = 1 << 2,
        Forbidden = 1 << 3,
        InsufficientResources = 1 << 4
    }
}