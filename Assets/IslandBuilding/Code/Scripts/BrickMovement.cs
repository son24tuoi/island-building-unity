using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BrickMovement : MonoBehaviour
    {
        [SerializeField] private BrickTrajectory brickTrajectory;
        [SerializeField][Range(0f, 1f)] private float timeStep;

        private float timeToMove;
        private float timeTrajectory;

        public void Setup(float timeToMove)
        {
            this.timeToMove = timeToMove;
            timeTrajectory = brickTrajectory.TotalTime;
        }

        public void Move(Action onComplete = null)
        {
            StartCoroutine(IEMove(onComplete));
        }

        private IEnumerator IEMove(Action onComplete)
        {
            float elapsedTime = 0;

            while (elapsedTime < timeToMove)
            {
                elapsedTime += Time.deltaTime;
                timeStep = elapsedTime / timeToMove;

                transform.position = brickTrajectory.GetPos(Mathf.Lerp(0f, timeTrajectory, timeStep));
                yield return null;
            }

            transform.position = brickTrajectory.GetPos(timeTrajectory);
            onComplete?.Invoke();
        }

#if UNITY_EDITOR
        private void OnValidate()
        {
            if (brickTrajectory == null)
                return;

            transform.position = brickTrajectory.GetPos(Mathf.Lerp(0f, brickTrajectory.TotalTime, timeStep));
        }
#endif
    }
}
