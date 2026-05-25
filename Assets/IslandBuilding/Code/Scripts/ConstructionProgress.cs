using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace One.IslandBuilding
{
    public class ConstructionProgress : MonoBehaviour
    {
        [SerializeField] private ConstructionArea constructionArea;

        private BuildingProgress currentBuiding;

        public bool IsDoneCurrentBuilding => currentBuiding.IsDone;

        public bool IsDone() => constructionArea.IsDone();

        public void LoadAllBuildingProgress(float[] datas) => constructionArea.LoadAllBuildingProgress(datas);

        private void Start()
        {
            SelectBuilding(0);
        }

        public void SelectBuilding(int index)
        {
            currentBuiding = constructionArea.GetBuildingProgress(index);
        }

        public void Build(float progress)
        {
            if (currentBuiding == null)
            {
                Debug.Log("current Building is null");
                return;
            }

            currentBuiding.Fill(progress);
        }
    }
}