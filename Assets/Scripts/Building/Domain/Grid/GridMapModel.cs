using System;

namespace GridCraft.Construction.Runtime
{
    [Flags]
    public enum GridCellState : byte
    {
        None = 0,
        Obstacle = 1 << 0,
        Forbidden = 1 << 1,
        Occupied = 1 << 2
    }

    public sealed class GridMapModel
    {
        private readonly GridCellState[] _cellStates;
        private readonly int _width;

        public GridMapModel(int width, int height, GridCell[] obstacleCells, GridCell[] forbiddenCells)
        {
            _width = width;
            _cellStates = new GridCellState[width * height];

            Fill(GridCellState.Obstacle, obstacleCells);
            Fill(GridCellState.Forbidden, forbiddenCells);
        }

        public bool IsForbidden(GridCell cell) => HasState(cell, GridCellState.Forbidden);

        public bool IsObstacle(GridCell cell) => HasState(cell, GridCellState.Obstacle);

        public bool IsOccupied(GridCell cell) => HasState(cell, GridCellState.Occupied);

        public void SetOccupied(GridCell origin, Footprint footprint, bool value)
        {
            for (var y = 0; y < footprint.Height; y++)
            for (var x = 0; x < footprint.Width; x++)
            {
                GridCell cell = new(origin.X + x, origin.Y + y);
                SetState(cell, GridCellState.Occupied, value);
            }
        }

        private bool HasState(GridCell cell, GridCellState state)
            => (_cellStates[cell.ToIndex(_width)] & state) != 0;

        private void SetState(GridCell cell, GridCellState state, bool value)
        {
            ref var cellState = ref _cellStates[cell.ToIndex(_width)];
            if (value)
            {
                cellState |= state;
                return;
            }

            cellState &= ~state;
        }

        private void Fill(GridCellState state, GridCell[] cells)
        {
            var count = cells.Length;
            for (var i = 0; i < count; i++)
                _cellStates[cells[i].ToIndex(_width)] |= state;
        }
    }
}