using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace One.IslandBuilding
{
    public class BrickTrajectory : MonoBehaviour
    {
        [Header("Config")]
        [SerializeField] private Transform start;
        [SerializeField] private Transform target;
        [SerializeField] private float h = 5f;
        [SerializeField] private float g = -9.81f;

        [Header("Datas")]
        private float t1 = 0.5f;
        private float t2 = 0.5f;
        private float totalTime = 1f;

        private Vector3 startPos;
        private Vector3 targetPos;

        private Vector3 velX;
        private Vector3 velY;
        private Vector3 velocity;

        private bool isValid;

        public float T1 => t1;
        public float T2 => t2;
        public float TotalTime => totalTime;

        public Vector3 StartPos => startPos;
        public Vector3 TargetPos => targetPos;

        public bool IsValid => isValid;

        public void Setup(Transform start, Transform target)
        {
            this.start = start;
            this.target = target;

            Setup();
        }

        [ContextMenu(nameof(Setup))]
        public void Setup()
        {
            startPos = start.position;
            targetPos = target.position;

            if (targetPos.y - startPos.y >= h)
            {
                isValid = false;
                Debug.LogError(nameof(BrickTrajectory) + ": Invalid h value");
                return;
            }

            isValid = true;

            Vector3 dXZ = new Vector3(targetPos.x - startPos.x, 0, targetPos.z - startPos.z);

            t1 = Mathf.Sqrt(-2 * h / g);
            t2 = Mathf.Sqrt(-2 * (h + (startPos.y - targetPos.y)) / g);
            totalTime = t1 + t2;

            velX = Vector3.up * Mathf.Sqrt(-2 * g * h);
            velY = dXZ / (t1 + t2);

            velocity = velX + velY;
        }

        public Vector3 GetPos(float t)
        {
            return startPos + velocity * t + 0.5f * g * t * t * Vector3.up;
        }

        public Vector3 GetHighestPos()
        {
            return GetPos(T1);
        }
    }
}
