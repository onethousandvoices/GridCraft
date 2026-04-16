using UnityEngine;

namespace GridCraft.Construction.Runtime
{
    public readonly record struct GridCell(int X, int Y)
    {
        public int ToIndex(int width) => Y * width + X;

        public static GridCell From(Vector2Int value) => new(value.x, value.y);
    }
}