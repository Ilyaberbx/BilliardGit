using UnityEngine;

namespace Logic
{
    public class BallThrower : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _power;

        private float _cameraRadians;
        private Vector3 _currentMousePosition;

        public void Throw(float force, Vector2 lastMousePosition, Vector3 currentMousePosition)
        {
            _currentMousePosition = currentMousePosition;
            _cameraRadians = CalculateCameraRadians();
            float angle = CalculateLenghtOfFingerPointPosition(lastMousePosition);

            Vector3 translatePosition = RotateToParallelCamera(lastMousePosition, angle, force);
            AddForce(force, lastMousePosition, translatePosition);
        }

        private void AddForce(float force, Vector2 lastMousePosition, Vector3 translatePosition)
        {
            float x = _power * force / 100 * (lastMousePosition.x - translatePosition.x);
            float y = _power * force / 100 * (translatePosition.y - lastMousePosition.y);
            _rigidbody.AddForce(x, 0, y);
        }

        private Vector2 RotateToParallelCamera(Vector2 lastMousePosition, float angle, float distance)
        {
            Vector2 translatePos;
            translatePos.x = Mathf.Sin(angle - _cameraRadians) * distance + lastMousePosition.x;
            translatePos.y = Mathf.Cos(angle - _cameraRadians) * distance + lastMousePosition.y;
            return translatePos;
        }

        private float CalculateLenghtOfFingerPointPosition(Vector2 lastMousePosition)
            => Mathf.Atan2(_currentMousePosition.y - lastMousePosition.y, _currentMousePosition.x - lastMousePosition.x);
        private float CalculateCameraRadians()
        {
            if (Camera.main != null)
                return (Camera.main.transform.localEulerAngles.y - 90) * Mathf.Deg2Rad;
            
            return 0;
        }
    }
}