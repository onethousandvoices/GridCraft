using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class GridCellHighlightView : MonoBehaviour
    {
        private static readonly Vector3 ModelOffset = new(0f, 0.12f, 0f);

        private readonly RendererMaterialState _materialState = new();

        private GameObject _modelInstance;

        public void Hide() => this.SetActiveSafe(false);

        public void Prepare(GameObject modelPrefab)
        {
            if (_modelInstance) return;

            _modelInstance = CodeUtilities.Instantiate(modelPrefab, transform);
            _modelInstance.name = modelPrefab.name;
            _modelInstance.transform.localPosition = ModelOffset;
            _modelInstance.transform.localRotation = Quaternion.identity;
            _modelInstance.transform.localScale = Vector3.one;

            var renderers = _modelInstance.GetComponentsInChildren<Renderer>(true);
            _materialState.Capture(renderers);
        }

        public void Show(Vector3 position, Material material)
        {
            transform.position = position;
            _materialState.Apply(material);
            this.SetActiveSafe(true);
        }
    }
}