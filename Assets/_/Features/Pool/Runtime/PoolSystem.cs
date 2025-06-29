using System.Collections.Generic;
using UnityEngine;

namespace Pool.Runtime
{
    public class PoolSystem
    {
        private readonly GameObject _prefab;
        private readonly Transform _parent;
        private readonly Queue<GameObject> _pool = new();

        public PoolSystem(GameObject prefab, int initialSize, Transform parent = null)
        {
            _prefab = prefab;
            _parent = parent;

            for (int i = 0; i < initialSize; i++)
            {
                CreateNewInstance();
            }
        }

        private GameObject CreateNewInstance()
        {
            GameObject instance = Object.Instantiate(_prefab, _parent);
            instance.SetActive(false);
            _pool.Enqueue(instance);
            return instance;
        }

        public GameObject Get()
        {
            if (_pool.Count == 0)
                CreateNewInstance();

            GameObject obj = _pool.Dequeue();
            obj.SetActive(true);
            return obj;
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}