using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class PlacedBuildingView : MonoBehaviour
    {
        private readonly RendererMaterialState _materialState = new();

        private void Awake()
            => _materialState.Capture(GetComponentsInChildren<Renderer>(true));

        public void Hide()
        {
            _materialState.Restore();
            this.SetActiveSafe(false);
        }

        public void Prepare() => _materialState.Restore();

        public void ShowGhost(Vector3 position, Quaternion rotation, Material material)
        {
            transform.SetPositionAndRotation(position, rotation);
            _materialState.Apply(material);
            this.SetActiveSafe(true);
        }

        public void ShowPlaced(Vector3 position, Quaternion rotation)
        {
            transform.SetPositionAndRotation(position, rotation);
            _materialState.Restore();
            this.SetActiveSafe(true);
        }
    }
}