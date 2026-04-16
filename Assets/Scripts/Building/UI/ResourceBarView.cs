using TMPro;
using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class ResourceBarView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _label;

        private void OnValidate() => CacheReferences();

        public void ShowBalance(int balance) => _label.text = $"Resources {balance}";

        private void CacheReferences()
        {
            var labels = GetComponentsInChildren<TextMeshProUGUI>(true);
            if (labels.Length > 0) _label = labels[0];
        }
    }
}