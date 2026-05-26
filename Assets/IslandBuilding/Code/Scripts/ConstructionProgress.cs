using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace One.IslandBuilding
{
    public class ConstructionProgress : MonoBehaviour
    {
        [SerializeField] private ConstructionArea constructionArea;

        [SerializeField] private BuildingProgress currentBuilding;

        [SerializeField] private int currentIndex = 0;

        [SerializeField] private bool isDone = false;

        public BuildingProgress CurrentBuilding => currentBuilding;

        public bool IsDone => isDone;

        public void LoadAllBuildingProgress(float[] datas) => constructionArea.LoadAllBuildingProgress(datas);

        private void Start()
        {
            constructionArea.Init();

            SetCurrentBuilding(0);

            isDone = constructionArea.IsDone();
        }

        public void SetCurrentBuilding(int index)
        {
            currentIndex = index;
            SelectBuilding(currentIndex);
        }

        public void SelectBuilding(int index)
        {
            currentBuilding = constructionArea.GetBuildingProgress(index);
        }

        public void Build(float fillRate)
        {
            if (isDone)
                return;

            if (currentBuilding == null)
            {
                Debug.Log("current Building is null");
                return;
            }

            currentBuilding.AddFill(fillRate);
        }

        public void BuildTarget(float fillRate)
        {
            if (isDone)
                return;

            if (currentBuilding == null)
            {
                Debug.Log("current Building is null");
                return;
            }

            currentBuilding.AddFillTarget(fillRate);

            if (currentBuilding.IsDone)
            {
                if (currentIndex < constructionArea.BuildingCount - 1)
                {
                    SetCurrentBuilding(++currentIndex);
                }
                else
                {
                    isDone = true;
                }
            }
        }
    }
}