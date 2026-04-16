using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GridCraft.Construction
{
    public static class CodeUtilities
    {
        public static GameObject Instantiate(Object original, Transform parent)
        {
            var instance = Object.Instantiate(original);
            return Attach(instance, parent);
        }

        public static void SetActiveSafe(this Component component, bool value)
        {
            if (!component) return;

            var gameObject = component.gameObject;
            if (gameObject.activeSelf != value) gameObject.SetActive(value);
        }

        private static GameObject Attach(Object instance, Transform parent)
        {
            if (instance is GameObject gameObject)
            {
                gameObject.transform.SetParent(parent, false);
                return gameObject;
            }

            if (instance is not Component component) throw new InvalidOperationException($"Cannot attach scene instance of type {instance.GetType().Name}");
            component.transform.SetParent(parent, false);
            return component.gameObject;

        }
    }
}