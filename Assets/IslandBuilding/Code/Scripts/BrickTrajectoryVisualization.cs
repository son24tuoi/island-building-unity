using System.Collections;
using System.Collections.Generic;
using One.IslandBuilding.Extensions;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BrickTrajectoryVisualization : MonoBehaviour
    {
        [SerializeField] private BrickTrajectory brickTrajectory;

#if UNITY_EDITOR
        [SerializeField] private float resolutionPath = 0.15f;

        public float TotalTime => brickTrajectory.TotalTime;

        public Vector3 StartPos => brickTrajectory.StartPos;
        public Vector3 TargetPos => brickTrajectory.TargetPos;

        private void OnDrawGizmos()
        {
            if (brickTrajectory == null || !brickTrajectory.IsValid)
                return;

            Gizmos.color = Color.green;

            Gizmos.DrawLine(StartPos, TargetPos);

            Gizmos.color = Color.yellow;

            Vector3 targetPosH = TargetPos.With(y: StartPos.y);
            Gizmos.DrawLine(StartPos, targetPosH);

            Gizmos.DrawLine(TargetPos, targetPosH);

            Vector3 highestPoint = brickTrajectory.GetHighestPos();
            Gizmos.DrawLine(highestPoint.With(y: StartPos.y), highestPoint);

            // Gizmos.color = Color.red;

            if (resolutionPath <= 0)
                return;

            Gizmos.color = Color.white;
            float t = 0f;
            while (t < TotalTime)
            {
                if (t + resolutionPath < TotalTime)
                {
                    Gizmos.DrawLine(GetPos(t), GetPos(t + resolutionPath));
                }
                else
                {
                    Gizmos.DrawLine(GetPos(t), GetPos(TotalTime));
                }

                t += resolutionPath;
            }
        }

        private Vector3 GetPos(float t) => brickTrajectory.GetPos(t);
#endif
    }
}
