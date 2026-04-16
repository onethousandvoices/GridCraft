using JetBrains.Annotations;
using UnityEngine;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class SceneBootstrapper : MonoBehaviour
    {
        private const string DEFAULT_SETTINGS_RESOURCE_PATH = "Construction/ConstructionSettings";
        private const string SETTINGS_RESOURCE_FOLDER_PATH = "Construction";

        [SerializeField] private Camera _mainCamera;
        [SerializeField] private ConstructionSettingsAsset _settingsAsset;

        private void Awake()
        {
            var sceneContext = ConstructionSceneBuilder.Build(_mainCamera, LoadSettingsAsset());
            ConstructionInstaller constructionInstaller = new(sceneContext);
            LifetimeScope.Create(constructionInstaller.Install, "SceneScope");
        }

        private ConstructionSettingsAsset LoadSettingsAsset()
        {
            if (_settingsAsset) return _settingsAsset;

            var settingsAsset = Resources.Load<ConstructionSettingsAsset>(DEFAULT_SETTINGS_RESOURCE_PATH);
            if (settingsAsset) return settingsAsset;

            var settingsAssets = Resources.LoadAll<ConstructionSettingsAsset>(SETTINGS_RESOURCE_FOLDER_PATH);
            return settingsAssets.Length > 0 ? settingsAssets[0] : null;
        }
    }
}

namespace System.Runtime.CompilerServices
{
    [UsedImplicitly]
    public static class IsExternalInit { }
}