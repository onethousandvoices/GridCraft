using UnityEngine;

namespace GridCraft.Construction
{
    public sealed class ConstructionSceneContext
    {
        public Camera MainCamera { get; }

        public ConstructionSettingsAsset SettingsAsset { get; }

        public LocationRootView LocationRootView { get; }

        public MainCanvasView MainCanvasView { get; }

        public ConstructionSceneContext(
            Camera mainCamera,
            ConstructionSettingsAsset settingsAsset,
            LocationRootView locationRootView,
            MainCanvasView mainCanvasView)
        {
            MainCamera = mainCamera;
            SettingsAsset = settingsAsset;
            LocationRootView = locationRootView;
            MainCanvasView = mainCanvasView;
        }
    }
}