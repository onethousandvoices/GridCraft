using System.Collections.Generic;
using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class RendererMaterialState
    {
        private readonly Dictionary<Material, Material[][]> _materialCache = new();

        private Material[][] _originalMaterials;
        private Renderer[] _renderers;
        private Material _currentMaterial;

        public void Capture(Renderer[] renderers)
        {
            _renderers = renderers;
            _originalMaterials = new Material[renderers.Length][];
            _materialCache.Clear();
            _currentMaterial = null;

            var count = renderers.Length;
            for (var i = 0; i < count; i++)
                _originalMaterials[i] = renderers[i].sharedMaterials;
        }

        public void Apply(Material material)
        {
            if (_renderers is null) return;
            if (_currentMaterial == material) return;

            if (!_materialCache.TryGetValue(material, out var materials))
            {
                materials = new Material[_renderers.Length][];
                for (var i = 0; i < _renderers.Length; i++)
                {
                    var subMeshCount = _renderers[i].sharedMaterials.Length;
                    var rendererMaterials = new Material[subMeshCount];
                    for (var j = 0; j < subMeshCount; j++)
                        rendererMaterials[j] = material;

                    materials[i] = rendererMaterials;
                }

                _materialCache.Add(material, materials);
            }

            for (var i = 0; i < _renderers.Length; i++)
                _renderers[i].sharedMaterials = materials[i];

            _currentMaterial = material;
        }

        public void Restore()
        {
            if (_renderers is null) return;
            if (!_currentMaterial) return;

            for (var i = 0; i < _renderers.Length; i++)
                _renderers[i].sharedMaterials = _originalMaterials[i];

            _currentMaterial = null;
        }
    }
}