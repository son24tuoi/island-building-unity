using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class UIController : MonoBehaviour
    {
        [SerializeField] private GameObject buildButton;
        [SerializeField] private GameObject resetButton;

        private void Awake()
        {
            ConstructionProgress.OnBuildDoneEvent += BuildDoneCallback;
        }

        private void Start()
        {
            buildButton.SetActive(true);
            resetButton.SetActive(false);
        }

        private void OnDestroy()
        {
            ConstructionProgress.OnBuildDoneEvent -= BuildDoneCallback;
        }

        private void BuildDoneCallback()
        {
            buildButton.SetActive(false);
            resetButton.SetActive(true);
        }
    }
}
