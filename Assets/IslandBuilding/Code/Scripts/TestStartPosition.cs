using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class TestStartPosition : MonoBehaviour
    {
        [SerializeField] private RectTransform buttonRT;
        [SerializeField] private float distance = 1f;

        [ContextMenu(nameof(GetPosUI))]
        public void GetPosUI()
        {
            // Debug.Log(buttonRT.transform.rotation.eulerAngles);
            // transform.position = Camera.main.ScreenToWorldPoint(buttonRT.transform.position);
            transform.position = buttonRT.transform.position + buttonRT.transform.forward * distance;
            // Vector3 worldPos;
            // Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(Camera.main, buttonRT.position);
            // RectTransformUtility.ScreenPointToWorldPointInRectangle(buttonRT, screenPoint, Camera.main, out worldPos);
            // transform.position = screenPoint;
        }
    }
}
