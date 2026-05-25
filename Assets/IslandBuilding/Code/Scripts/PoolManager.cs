using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public enum PoolType
    {
        Brick = 0,
    }

    public class PoolManager : MonoBehaviour
    {
        [SerializeField] private Pool[] pools;

        public static PoolManager Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
                Init();
            }
            else
            {
                Destroy(gameObject);
            }
        }

        public void Init()
        {
            for (int i = 0; i < pools.Length; i++)
            {
                pools[i].Init(transform);
            }
        }

        public bool IsValidPool(int index)
        {
            return index >= 0 && index <= pools.Length - 1;
        }

        public GameObject Get(PoolType poolType)
        {
            int index = (int)poolType;

            if (!IsValidPool(index))
                return null;

            return pools[index].Get();
        }

        public T Get<T>(PoolType poolType) where T : Component
        {
            int index = (int)poolType;

            if (!IsValidPool(index))
                return null;

            if (pools[index].Get().TryGetComponent<T>(out var obj))
            {
                return obj;
            }

            ReturnToPool(poolType, obj.gameObject);
            return null;
        }

        public bool ReturnToPool(PoolType poolType, GameObject obj)
        {
            int index = (int)poolType;

            if (!IsValidPool(index))
                return false;

            pools[index].ReturnToPool(obj);
            return true;
        }

        public bool ReturnToPool<T>(PoolType poolType, T obj) where T : Component
        {
            int index = (int)poolType;

            if (!IsValidPool(index))
                return false;

            pools[index].ReturnToPool(obj.gameObject);
            return true;
        }
    }
}