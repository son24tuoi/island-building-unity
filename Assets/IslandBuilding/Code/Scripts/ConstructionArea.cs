using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace One.IslandBuilding
{
    public class ConstructionArea : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private BuildingProgress[] buildingProgressArray;
        [SerializeField] private DecorationObject[] decorationObjects;

        public int BuildingCount => buildingProgressArray.Length;

        [ContextMenu(nameof(GetElements))]
        private void GetElements()
        {
            List<BuildingProgress> buildingProgressList = new List<BuildingProgress>();
            List<DecorationObject> decorationObjectList = new List<DecorationObject>();

            for (int i = 0; i < transform.childCount; i++)
            {
                if (transform.GetChild(i).TryGetComponent(out BuildingProgress buildingProgress))
                {
                    buildingProgressList.Add(buildingProgress);
                }
                else if (transform.GetChild(i).TryGetComponent(out DecorationObject decorationObject))
                {
                    decorationObjectList.Add(decorationObject);
                }
            }

            buildingProgressArray = buildingProgressList.ToArray();
            decorationObjects = decorationObjectList.ToArray();
        }

        public void Init()
        {
            for (int i = 0; i < buildingProgressArray.Length; i++)
            {
                buildingProgressArray[i].Init();
            }
        }

        public BuildingProgress GetBuildingProgress(int index)
        {
            if (IsValidBuilding(index))
            {
                return buildingProgressArray[index];
            }

            return null;
        }

        public bool IsValidBuilding(int index)
        {
            if (buildingProgressArray == null)
                return false;

            if (index < 0 || index >= buildingProgressArray.Length)
                return false;

            return true;
        }

        public bool IsDone()
        {
            for (int i = 0; i < buildingProgressArray.Length; i++)
            {
                if (!buildingProgressArray[i].IsDone)
                {
                    return false;
                }
            }

            return true;
        }

        public void LoadAllBuildingProgress(float[] datas)
        {
            for (int i = 0; i < datas.Length; i++)
            {
                if (IsValidBuilding(i))
                {
                    buildingProgressArray[i].Fill(datas[i]);
                }
                else
                {
                    break;
                }
            }

            if (datas.Length < buildingProgressArray.Length)
            {
                LoadRangeBuildingProgress(datas.Length, buildingProgressArray.Length, 0f);
            }
        }

        /// <summary>
        /// Load range building progress within [startIndex..endIndex)
        /// </summary>
        public void LoadRangeBuildingProgress(int startIndex, int endIndex, float data)
        {
            for (int i = startIndex; i < endIndex; i++)
            {
                if (IsValidBuilding(i))
                {
                    buildingProgressArray[i].Fill(data);
                }
            }
        }
    }
}