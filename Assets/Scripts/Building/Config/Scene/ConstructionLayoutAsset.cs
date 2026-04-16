using System;
using GridCraft.Construction.Runtime;
using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Layout", fileName = "ConstructionLayout")]
    public sealed class ConstructionLayoutAsset : ScriptableObject
    {
        [SerializeField] private int _gridWidth = 8;
        [SerializeField] private int _gridHeight = 10;
        [SerializeField] private float _cellSize = 1f;
        [SerializeField] private Vector3 _gridOrigin;
        [SerializeField] private Vector2Int[] _rockObstacleCells = Array.Empty<Vector2Int>();
        [SerializeField] private Vector2Int[] _treeObstacleCells = Array.Empty<Vector2Int>();
        [SerializeField] private Vector2Int[] _crystalObstacleCells = Array.Empty<Vector2Int>();
        [SerializeField] private Vector2Int[] _forbiddenCells = Array.Empty<Vector2Int>();

        public int GridWidth => _gridWidth;

        public int GridHeight => _gridHeight;

        public float CellSize => _cellSize;

        public Vector3 GridOrigin => _gridOrigin;

        private void OnValidate()
        {
            if (_gridWidth < 1) _gridWidth = 1;
            if (_gridHeight < 1) _gridHeight = 1;
            if (_cellSize <= 0f) _cellSize = 0.01f;
        }

        public GridCell[] CreateForbiddenCells()
        {
            var result = new GridCell[_forbiddenCells.Length];
            var count = _forbiddenCells.Length;
            for (var i = 0; i < count; i++)
                result[i] = GridCell.From(_forbiddenCells[i]);

            return result;
        }

        public GridCell[] CreateObstacleCells()
        {
            var totalCount = _rockObstacleCells.Length + _treeObstacleCells.Length + _crystalObstacleCells.Length;
            var result = new GridCell[totalCount];
            var index = 0;

            Copy(_rockObstacleCells, result, ref index);
            Copy(_treeObstacleCells, result, ref index);
            Copy(_crystalObstacleCells, result, ref index);

            return result;
        }

        private static void Copy(Vector2Int[] source, GridCell[] destination, ref int index)
        {
            var count = source.Length;
            for (var i = 0; i < count; i++)
            {
                destination[index] = GridCell.From(source[i]);
                index++;
            }
        }
    }
}