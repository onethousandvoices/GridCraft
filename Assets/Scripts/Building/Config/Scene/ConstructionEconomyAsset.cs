using UnityEngine;

namespace GridCraft.Construction
{
    [CreateAssetMenu(menuName = "Construction/Economy", fileName = "ConstructionEconomy")]
    public sealed class ConstructionEconomyAsset : ScriptableObject
    {
        [SerializeField] private int _startingResources = 1000;

        public int StartingResources => _startingResources;
    }
}