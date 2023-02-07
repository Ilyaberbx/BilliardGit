using UnityEngine;

namespace Logic
{
    [RequireComponent(typeof(LineRenderer))]
    public class BallLineDrawer : MonoBehaviour
    {
        [SerializeField] private LineRenderer _lineRenderer;
        [SerializeField] private Scaler _scaler;
        [SerializeField] private float _projectionLenght;

        private float _lineLenght;

        private Vector3 _secondPosition;
        private Vector3 _thirdPosition;

        private void OnDisable() 
            => _lineRenderer.enabled = false;

        private void Awake() 
            => _lineRenderer.enabled = false;

        public void DrawLine()
        {
            CalculateLinePositions();
            UpdateLineRendererPositions();
        }

        private void CalculateLinePositions()
        {
            if (!HitRay(out RaycastHit hit)) return;

            _lineLenght = CalculateLineLenght(hit);
            _secondPosition = new Vector3(0.0f, 0.0f, _lineLenght);
            _lineRenderer.positionCount = 2;
            
            
            if (IsBall(hit))
            {
                _lineRenderer.positionCount = 3;
                CalculateHitBallTrajectory(hit);
            }
        }

        private bool IsBall(RaycastHit hit) 
            => hit.transform.TryGetComponent(out Ball _);

        private void CalculateHitBallTrajectory(RaycastHit hit)
        {
            hit.transform.SetParent(transform);
            Vector3 ballCenter = hit.transform.localPosition;

            Vector3 hitTrajectory = CalculateHitTrajectory(ballCenter);
            hitTrajectory *= _projectionLenght;
            _thirdPosition = hitTrajectory + _secondPosition;
            
            hit.transform.SetParent(null);
        }

        private Vector3 CalculateHitTrajectory(Vector3 ballCenter)
        {
            var hitTrajectory = ballCenter - _secondPosition;
            hitTrajectory.Normalize();
            return hitTrajectory;
        }

        private bool HitRay(out RaycastHit hit) =>
            Physics.Raycast(transform.position, transform.TransformDirection(-Vector3.forward), out hit,
                Mathf.Infinity);

        private float CalculateLineLenght(RaycastHit hit)
        {
            float ballRadius = _scaler.Scale / 2;
            return -hit.distance / _scaler.Scale + ballRadius;
        }
        

        private void UpdateLineRendererPositions()
        {
            _lineRenderer.enabled = true;
            _lineRenderer.SetPosition(0, Vector3.zero);
            _lineRenderer.SetPosition(1, _secondPosition);

            if (_lineRenderer.positionCount == 3)
            {
                _lineRenderer.SetPosition(2, _thirdPosition);
            }
        }
    }
}