using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BuildingMaterials : MonoBehaviour
    {
        [SerializeField] private Renderer mainRenderer;
        [SerializeField] private MeshFilter meshFilter;

        private static readonly int FillProgressID = Shader.PropertyToID("_Fill_Progress");
        private static readonly int HeightID = Shader.PropertyToID("_Height");

        private int amountMaterials;

        [ContextMenu(nameof(GetElements))]
        private void GetElements()
        {
            mainRenderer = GetComponent<Renderer>();
            meshFilter = GetComponent<MeshFilter>();
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            amountMaterials = mainRenderer.materials.Length;
            UpdateHeightMaterials(GetHeightMesh());
        }

        public float GetHeightMesh()
        {
            if (meshFilter == null)
                return 0f;

            return meshFilter.mesh.bounds.center.y * 2;
        }

        public void UpdateFillMaterials(float fillPercent)
        {
            for (int i = 0; i < amountMaterials; i++)
            {
                mainRenderer.materials[i].SetFloat(FillProgressID, fillPercent);
            }
        }

        public void UpdateHeightMaterials(float height)
        {
            for (int i = 0; i < amountMaterials; i++)
            {
                mainRenderer.materials[i].SetFloat(HeightID, height);
            }
        }
    }
}
