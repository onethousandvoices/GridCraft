using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class LocationRootView : MonoBehaviour
    {
        [SerializeField] private Transform _dynamicRoot;
        [SerializeField] private Transform _highlightRoot;

        public Transform DynamicRoot => _dynamicRoot;

        public Transform HighlightRoot => _highlightRoot;
    }
}