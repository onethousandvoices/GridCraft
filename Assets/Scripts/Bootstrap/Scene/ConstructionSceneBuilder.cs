using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.UI;
using Object = UnityEngine.Object;

namespace GridCraft.Construction
{
    public static class ConstructionSceneBuilder
    {
        public static ConstructionSceneContext Build(Camera mainCamera, ConstructionSettingsAsset settingsAsset)
        {
            EnsureEventSystem();

            var mainCanvasView = CreateMainCanvasView(settingsAsset.Scene);
            var locationRootView = CreateLocationRootView(settingsAsset.Scene, settingsAsset.Layout);
            return new(mainCamera, settingsAsset, locationRootView, mainCanvasView);
        }

        private static MainCanvasView CreateMainCanvasView(ConstructionSceneAsset constructionSceneAsset)
        {
            var mainCanvasPrefab = constructionSceneAsset.MainCanvasPrefab;
            var mainCanvasView = Object.Instantiate(mainCanvasPrefab);
            mainCanvasView.name = mainCanvasPrefab.name;
            return mainCanvasView;
        }

        private static LocationRootView CreateLocationRootView(ConstructionSceneAsset constructionSceneAsset, ConstructionLayoutAsset constructionLayoutAsset)
        {
            var locationRootView = Object.Instantiate(constructionSceneAsset.LocationPrefab);
            locationRootView.name = constructionSceneAsset.LocationPrefab.name;
            locationRootView.transform.SetPositionAndRotation(constructionLayoutAsset.GridOrigin, Quaternion.identity);
            return locationRootView;
        }

        private static void EnsureEventSystem()
        {
            if (EventSystem.current) return;

            GameObject eventSystemObject = new("EventSystem", typeof(EventSystem), typeof(InputSystemUIInputModule));
            var inputSystemUiInputModule = eventSystemObject.GetComponent<InputSystemUIInputModule>();
            inputSystemUiInputModule.AssignDefaultActions();
        }
    }
}