using System;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace GridCraft.Construction
{
    public sealed class ComponentPool<TKey, TView>
        where TView : Component
    {
        private readonly Dictionary<TKey, Stack<TView>> _availableByKey = new();
        private readonly Dictionary<TView, TKey> _keyByInstance = new();
        private readonly Transform _parent;
        private readonly IObjectResolver _resolver;

        public ComponentPool(IObjectResolver resolver, Transform parent)
        {
            _resolver = resolver;
            _parent = parent;
        }

        public TView Rent(TKey key, TView prefab)
        {
            if (_availableByKey.TryGetValue(key, out var pool) && pool.Count > 0)
            {
                var pooledView = pool.Pop();
                pooledView.transform.SetParent(_parent, false);
                return pooledView;
            }

            var instance = _resolver.Instantiate(prefab, _parent);
            _keyByInstance[instance] = key;
            return instance;
        }

        public void Prewarm(TKey key, TView prefab, int count, Action<TView> setup)
        {
            if (count <= 0) return;

            if (!_availableByKey.TryGetValue(key, out var pool))
            {
                pool = new();
                _availableByKey.Add(key, pool);
            }

            var missingCount = count - pool.Count;
            for (var i = 0; i < missingCount; i++)
            {
                var instance = _resolver.Instantiate(prefab, _parent);
                _keyByInstance[instance] = key;
                setup?.Invoke(instance);
                instance.SetActiveSafe(false);
                pool.Push(instance);
            }
        }

        public void Return(TView view)
        {
            var key = _keyByInstance[view];
            if (!_availableByKey.TryGetValue(key, out var pool))
            {
                pool = new();
                _availableByKey.Add(key, pool);
            }

            view.transform.SetParent(_parent, false);
            pool.Push(view);
        }
    }
}