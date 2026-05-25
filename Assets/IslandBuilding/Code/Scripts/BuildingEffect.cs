using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BuildingEffect : MonoBehaviour
    {
        [SerializeField] private Animator animator;

        private static readonly int _bounceParameterHash = Animator.StringToHash("Bounce");

        [ContextMenu(nameof(GetElements))]
        private void GetElements()
        {
            animator = GetComponent<Animator>();
        }

        public void PlayBounce()
        {
            animator.Play(_bounceParameterHash);
        }
    }
}
