using System.Collections;
using System.Collections.Generic;
using One.IslandBuilding.Extensions;
using UnityEngine;

namespace One.IslandBuilding
{
    public class TestTrajectory : MonoBehaviour
    {
        [SerializeField] private Transform start;
        [SerializeField] private Transform target;
        [SerializeField] private Transform ball;
        [SerializeField] private float totalTime = 1f;

        [SerializeField][Range(0f, 1f)] private float timeStep;

        [SerializeField] private float h = 5f;
        [SerializeField] private float t1 = 0.5f;
        [SerializeField] private float t2 = 0.5f;

        [SerializeField] private float g;
        private Vector3 _startPos;
        private Vector3 _targetPos;
        private Vector3 _targetPosH;
        private Vector3 _midPointH;
        [SerializeField] private Vector3 _highestPoint;
        private Vector3 _vel1;
        private Vector3 _vel2;

        [SerializeField] private float resolutionPath;
        [SerializeField] private Vector3 _velocity;
        [SerializeField] private Vector3 _velRotated;
        [SerializeField] private Vector3 _velX;
        [SerializeField] private Vector3 _velY;
        [SerializeField] Vector3 u1;
        [SerializeField] Vector3 u2;
        [SerializeField] Vector3 horizontal;
        [SerializeField] Vector3 vertical;
        [SerializeField] Vector3 n;
        [SerializeField] float d;

        private void OnEnable()
        {

        }

        private void OnDisable()
        {

        }

        [ContextMenu(nameof(Init))]
        public void Init()
        {
            _startPos = start.position;
            _targetPos = target.position;

            _targetPosH = _targetPos.With(y: _startPos.y);
            _midPointH = (_startPos + _targetPosH) / 2;

            g = -9.81f;
            // g = FindGravity(h + Vector3.Distance(_targetPos, _targetPosH), t2);
            d = Vector3.Distance(_startPos, _targetPos);
            t2 = Mathf.Sqrt(2 * (h + Vector3.Distance(_targetPos, _targetPosH)) / g);
            // _g = Physics.gravity.y;

            // _velocity = GetVelocity(_startPos, _targetPos, timeToMove);

            u2 = _targetPos - _startPos;
            u1 = GetPoint(_startPos, u2, 5f).With(y: _startPos.y) - _startPos;
            n = GetCrossProduct(u1, u2);

            horizontal = u1;
            vertical = Vector3.up;

            // float alpha = Vector3.Angle(u1, u2);
            // float height = h + d * Mathf.Sin(alpha);

            float dY = _targetPos.y - _startPos.y;
            Vector3 dXZ = new Vector3(_targetPos.x - _startPos.x, 0, _targetPos.z - _startPos.z);

            t1 = Mathf.Sqrt(-2 * h / g);
            t2 = Mathf.Sqrt(-2 * (h + (_startPos.y - _targetPos.y)) / g);
            totalTime = t1 + t2;

            _velX = Vector3.up * Mathf.Sqrt(-2 * g * h);
            _velY = dXZ / (t1 + t2);

            _velocity = _velX + _velY;

            _highestPoint = GetPos(t1);
        }

        // [ContextMenu(nameof(StartMove))]
        // public void StartMove()
        // {
        //     StartCoroutine(IEMove(timeToMove));
        // }

        // private IEnumerator IEMove(float timeToMove)
        // {
        //     float elapsedTime = 0f;
        //     float t;
        //     while (true)
        //     {
        //         if ((transform.position - _targetPos).sqrMagnitude < 0.0001f)
        //         {
        //             break;
        //         }

        //         elapsedTime += Time.deltaTime;
        //         t = elapsedTime / timeToMove;

        //         start.position = _startPos + (_velocity * t) - (_g * t * t);

        //         yield return null;
        //     }

        //     Debug.Log("Finish Move");
        // }

        private void OnValidate()
        {
            ball.position = GetPos(Mathf.Lerp(0f, t1 + t2, timeStep));
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawLine(_startPos, _targetPos);

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(_startPos, _targetPosH);
            Gizmos.DrawLine(_targetPos, _targetPosH);
            Gizmos.DrawLine(_highestPoint.With(y: _startPos.y), _highestPoint);

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_startPos, GetPoint(_startPos, n, 5f));

            Gizmos.color = Color.magenta;
            Gizmos.DrawLine(_startPos, GetPoint(_startPos, Vector3.up, 5f));

            if (resolutionPath <= 0)
                return;

            Gizmos.color = Color.white;
            float t = 0f;
            while (t < totalTime)
            {
                if (t + resolutionPath < totalTime)
                {
                    Gizmos.DrawLine(GetPos(t), GetPos(t + resolutionPath));
                }
                else
                {
                    Gizmos.DrawLine(GetPos(t), GetPos(totalTime));
                }

                t += resolutionPath;
            }
        }

        private Vector3 GetCrossProduct(Vector3 a, Vector3 b)
        {
            return new Vector3(
                a.y * b.z - a.z * b.y,
                a.z * b.x - a.x * b.z,
                a.x * b.y - a.y * b.x
            );
        }

        private Vector3 GetPoint(Vector3 originPoint, Vector3 u, float d)
        {
            return originPoint + u.normalized * d;
        }

        private Vector3 GetRotatedVector(Vector3 initialVector, Vector3 rotationAxis, float rotationAngle)
        {
            return Quaternion.AngleAxis(rotationAngle, rotationAxis) * initialVector;
        }

        private Vector3 GetPos(float t)
        {
            // Vector3 posX = _velX * t;
            // Vector3 posY = (t <= t2) ?
            //     _velY * t - 0.5f * g * Vector3.up * t * t :
            //     -0.5f * g * Vector3.up * t * t;

            // return _startPos + posX + posY;

            // return (t <= t2) ?
            //     _startPos + _vel1 * t - 0.5f * g * Vector3.up * t * t :
            //     _highestPoint;

            return _velocity * t + 0.5f * g * t * t * Vector3.up + _startPos;
        }

        private Vector3 GetVelocity(Vector3 start, Vector3 target, float timeToMove)
        {
            return (target - start) / timeToMove + 0.5f * Vector3.up * g;
        }

        private float FindGravity(float height, float t2)
        {
            return 2 * height / (t2 * t2);
        }
    }
}