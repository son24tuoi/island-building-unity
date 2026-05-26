using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BuildingProgress : MonoBehaviour
    {
        [SerializeField] private BuildingEffect buildingEffect;
        [SerializeField] private BuildingMaterials buildingMaterials;

        [SerializeField][Range(0, 1)] private float fillPercent;
        [SerializeField][Range(0, 1)] private float fillPercentTarget;

        private bool isDone;

        private Transform tf;

        public Transform Transform
        {
            get
            {
                if (tf == null)
                    tf = transform;
                return tf;
            }
        }

        public float FillPercent => fillPercent;

        public float FillPercentTarget => fillPercentTarget;

        public bool IsDone => isDone;

        [ContextMenu(nameof(GetBuidingScript))]
        private void GetBuidingScript()
        {
            buildingEffect = GetComponent<BuildingEffect>();
            buildingMaterials = GetComponent<BuildingMaterials>();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            Fill(fillPercent);
        }
#endif

        [ContextMenu(nameof(Init))]
        public void Init()
        {
            buildingMaterials.Setup();
        }

        public void Fill(float fillPercent)
        {
            if (this.fillPercent >= 1f)
                return;

            this.fillPercent = Mathf.Clamp01(fillPercent);
            buildingMaterials.UpdateFillMaterials(fillPercent);
            buildingEffect.PlayBounce();
        }

        public void FillTarget(float fillPercentTarget)
        {
            if (isDone)
                return;

            this.fillPercentTarget = Mathf.Clamp01(fillPercentTarget);

            if (this.fillPercentTarget >= 1f)
            {
                isDone = true;
            }
        }

        public void AddFill(float fillRate) => Fill(fillPercent + fillRate);

        public void AddFillTarget(float fillRate) => FillTarget(fillPercentTarget + fillRate);
    }
}
