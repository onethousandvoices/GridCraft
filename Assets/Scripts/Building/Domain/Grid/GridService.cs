using UnityEngine;

namespace GridCraft.Construction.Runtime
{
    public sealed class GridService
    {
        private readonly int _width;
        private readonly int _height;
        private readonly float _cellSize;
        private readonly Vector3 _origin;

        public Vector3 Origin => _origin;

        public GridService(int width, int height, float cellSize, Vector3 origin)
        {
            _width = width;
            _height = height;
            _cellSize = cellSize;
            _origin = origin;
        }

        public bool Contains(GridCell cell)
            => cell is { X: >= 0, Y: >= 0 }
               && cell.X < _width
               && cell.Y < _height;

        public Vector3 GetCellCenter(GridCell cell)
            => new(
                _origin.x + (cell.X + 0.5f) * _cellSize,
                _origin.y,
                _origin.z + (cell.Y + 0.5f) * _cellSize);

        public Vector3 GetPlacementPosition(GridCell origin, Footprint footprint)
            => new(
                _origin.x + (origin.X + footprint.Width * 0.5f) * _cellSize,
                _origin.y,
                _origin.z + (origin.Y + footprint.Height * 0.5f) * _cellSize);

        public bool TryGetCell(Vector3 worldPosition, out GridCell cell)
        {
            var localPosition = worldPosition - _origin;
            var x = Mathf.FloorToInt(localPosition.x / _cellSize);
            var y = Mathf.FloorToInt(localPosition.z / _cellSize);

            cell = new(x, y);
            return Contains(cell);
        }
    }
}