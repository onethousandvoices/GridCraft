using GridCraft.Construction.Runtime;
using UnityEngine;

namespace GridCraft.Construction
{
    public interface IScreenWorldProjector
    {
        bool TryProject(Vector2 screenPosition, out Vector3 worldPosition);
    }

    public sealed class TouchWorldProjector : IScreenWorldProjector
    {
        private readonly Camera _camera;
        private readonly Plane _plane;

        public TouchWorldProjector(Camera camera, GridService gridService)
        {
            _camera = camera;
            _plane = new(Vector3.up, gridService.Origin);
        }

        public bool TryProject(Vector2 screenPosition, out Vector3 worldPosition)
        {
            var ray = _camera.ScreenPointToRay(screenPosition);
            if (_plane.Raycast(ray, out var distance))
            {
                worldPosition = ray.GetPoint(distance);
                return true;
            }

            worldPosition = default;
            return false;
        }
    }
}