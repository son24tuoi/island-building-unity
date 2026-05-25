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

        public bool IsDone
        {
            get => fillPercent >= 1f;
        }

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
            if (fillPercent > 1f || fillPercent < 0f)
                return;

            this.fillPercent = fillPercent;
            buildingMaterials.UpdateFillMaterials(fillPercent);
            buildingEffect.PlayBounce();
        }
    }
}
