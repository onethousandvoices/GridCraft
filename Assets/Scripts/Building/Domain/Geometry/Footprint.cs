namespace GridCraft.Construction.Runtime
{
    public readonly record struct Footprint(int Width, int Height)
    {
        public int Area => Width * Height;
    }
}