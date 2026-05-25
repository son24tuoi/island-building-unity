using System;
using UnityEngine;

namespace One.IslandBuilding
{
    public class Brick : MonoBehaviour
    {
        [SerializeField] private BrickMovement movement;
        [SerializeField] private BrickTrajectory trajectory;

        private PoolManager poolManager;

        public PoolManager PoolManager
        {
            get
            {
                if (poolManager == null)
                    poolManager = PoolManager.Instance;
                return poolManager;
            }
        }

        public void Setup(Transform start, Transform target, Action onComplete = null)
        {
            gameObject.SetActive(true);

            trajectory.Setup(start, target);
            movement.Setup(trajectory.TotalTime);

            movement.Move(() =>
            {
                PoolManager.ReturnToPool(PoolType.Brick, gameObject);
                onComplete?.Invoke();
            });
        }
    }
}