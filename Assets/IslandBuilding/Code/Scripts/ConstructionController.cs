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
        
        [SerializeField][Range(0f, 1f)] private float fillRate = 0.01f;

        private Brick GetBrick() => poolManager.Get<Brick>(PoolType.Brick);

        public void LaunchBrick()
        {
            if (constructionProgress.IsDone)
                return;

            Brick brick = GetBrick();
            brick.Setup(start, constructionProgress.CurrentBuilding.Transform, Build);

            constructionProgress.BuildTarget(fillRate);
        }

        private void Build()
        {
            constructionProgress.Build(fillRate);
        }
    }
}
