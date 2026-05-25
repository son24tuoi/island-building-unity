using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

namespace One.IslandBuilding
{
    public class BuildButton : MonoBehaviour
    {
        [Header("Elements")]
        [SerializeField] private ConstructionController constructionController;
        [SerializeField] private float holdTimeThreshold = 0.5f;
        [SerializeField] private bool isUpdating;

        private float holdTime;
        private Coroutine updateRoutine;

        public void OnPointerDown()
        {
            // Debug.Log("Pointer Down");
            constructionController.LaunchBrick();
            holdTime = 0f;
            updateRoutine = StartCoroutine(IEUpdate());
        }

        public void OnPointerUp()
        {
            // Debug.Log("Pointer Up");
            holdTime = 0f;
            isUpdating = false;
            StopCoroutine(updateRoutine);
        }

        private IEnumerator IEUpdate()
        {
            isUpdating = true;
            while (isUpdating)
            {
                holdTime += Time.deltaTime;

                if (holdTime >= holdTimeThreshold)
                {
                    constructionController.LaunchBrick();
                    holdTime = 0f;
                }
                yield return null;
            }
            isUpdating = false;
        }
    }
}
