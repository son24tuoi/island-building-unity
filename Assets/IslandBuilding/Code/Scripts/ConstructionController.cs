using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class ConstructionController : MonoBehaviour
    {
        [SerializeField] private ConstructionProgress constructionProgress;
        [SerializeField] private PoolManager poolManager;

        [SerializeField] private Transform start;
        [SerializeField] private Transform target;

        private float progress = 0f;

        private Brick GetBrick() => poolManager.Get<Brick>(PoolType.Brick);

        public void LaunchBrick()
        {
            Brick brick = GetBrick();
            brick.Setup(start, target, Build);
        }

        private void Build()
        {
            constructionProgress.Build(progress += 0.01f);
        }
    }
}
