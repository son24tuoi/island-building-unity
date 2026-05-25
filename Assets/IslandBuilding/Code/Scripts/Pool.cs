using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    [Serializable]
    public class Pool
    {
        [SerializeField] private GameObject prefab;
        [SerializeField] private int size;

        private Transform parent;
        private Queue<GameObject> objects;

        public void Init(Transform parent)
        {
            this.parent = parent;
            objects = new Queue<GameObject>();
            AddObjects(size, parent);
        }

        public GameObject Get()
        {
            if (objects.Count == 0)
                AddObjects(1, parent);

            return objects.Dequeue();
        }

        public void ReturnToPool(GameObject obj)
        {
            obj.SetActive(false);
            objects.Enqueue(obj);
        }

        public void AddObjects(int count, Transform parent)
        {
            if (parent == null)
                return;

            for (int i = 0; i < count; i++)
            {
                GameObject newObj = GameObject.Instantiate(prefab, parent);
                newObj.SetActive(false);
                objects.Enqueue(newObj);
            }
        }
    }
}