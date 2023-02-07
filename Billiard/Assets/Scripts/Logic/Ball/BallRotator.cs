using System;
using UnityEngine;

namespace Logic
{
    public class BallRotator : MonoBehaviour
    {
        private Camera _viewCamera;
        private Vector3 _mousePoint;

        private void Awake() 
            => _viewCamera = Camera.main;

        public void RotateToTrajectoryDirection()
        {
            _mousePoint = CalculateMousePosition();
            LookAtDirection();
        }
        private Vector3 CalculateMousePosition()
            => _viewCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x,
                Input.mousePosition.y, _viewCamera.transform.position.y));
        private void LookAtDirection()
            => transform.LookAt(_mousePoint + Vector3.up * transform.position.y);
    }
}