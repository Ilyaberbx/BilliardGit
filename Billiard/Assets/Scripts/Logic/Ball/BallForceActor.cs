using System;
using Infrastructure.Service.Input;
using UnityEngine;

namespace Logic
{
    public class BallForceActor : MonoBehaviour
    {
        public event Action<float> OnForceChanged;

        [SerializeField] private BallLineDrawer _lineDrawer;
        [SerializeField] private BallThrower _ballThrower;
        [SerializeField] private BallRotator _ballRotator;
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float _maxPower;
        [SerializeField] private float _forceChangingMultiplier;

        private IInputService _inputService;
        private Vector2 _currentMousePosition;
        private Vector2 _previousMousePosition;
        private bool _isMouseHolding;
        private Camera _viewCamera;
        private float _force;

        public void Construct(IInputService inputService)
            => _inputService = inputService;

        private void Awake()
        {
            _viewCamera = Camera.main;
            _lineDrawer.enabled = false;
        }

        private void Update()
        {
            if (_inputService.IsMouseDown())
            {
                bool hit = Raycast(out RaycastHit hitInfo);

                if (hit)
                {
                    if (IsPlayer(hitInfo))
                    {
                        _isMouseHolding = true;
                        _lineDrawer.enabled = true;
                        _rigidbody.Sleep();
                        TraceMousePosition();
                    }
                }
            }

            if (_isMouseHolding)
            {
                _currentMousePosition = MousePosition();
                _force = CalculateForce();
                _ballRotator.RotateToTrajectoryDirection();
                _lineDrawer.DrawLine();
                OnForceChanged?.Invoke(_force);
            }

            if (_inputService.IsMouseUp())
            {
                if (_isMouseHolding)
                    ThrowBall();

                InterruptForceCalculation();
            }
        }

        private void InterruptForceCalculation()
        {
            _isMouseHolding = false;
            _lineDrawer.enabled = false;
            _force = 0;
        }

        private bool IsPlayer(RaycastHit hitInfo)
            => hitInfo.transform.TryGetComponent(out BallForceActor _);

        private bool Raycast(out RaycastHit hitInfo)
            => Physics.Raycast(_viewCamera.ScreenPointToRay(Input.mousePosition), out hitInfo);

        private Vector2 MousePosition()
            => new Vector2(Input.mousePosition.x, Input.mousePosition.y);

        private float CalculateForce()
            => Mathf.Clamp(DistanceBetweenMousePoints(), 0, _maxPower) / _forceChangingMultiplier;

        private float DistanceBetweenMousePoints()
            => Vector2.Distance(_previousMousePosition, _currentMousePosition);

        private void TraceMousePosition()
        {
            transform.rotation = Quaternion.identity;
            _previousMousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        }

        private void ThrowBall()
        {
            _rigidbody.WakeUp();
            _ballThrower.Throw(_force, _previousMousePosition,_currentMousePosition);
            OnForceChanged?.Invoke(0);
        }
    }
}