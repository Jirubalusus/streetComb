using System;
using UnityEngine;

namespace StreetComb.InputSystem
{
    public class TouchCapture : MonoBehaviour
    {
        public event Action<GestureSample> OnGestureCompleted;

        [SerializeField] private bool useMouseInEditor = true;
        [SerializeField] private float doubleTapWindow = 0.28f;
        [SerializeField] private float tapMaxDistance = 25f;
        [SerializeField] private float tapMaxDuration = 0.18f;

        private readonly GestureSample _currentSample = new();
        private bool _isCapturing;
        private float _lastTapTime = -10f;
        private Vector2 _lastTapPosition;

        public GestureSample CurrentSample => _currentSample;
        public bool IsCapturing => _isCapturing;

        private void Update()
        {
#if UNITY_EDITOR || UNITY_STANDALONE
            if (useMouseInEditor)
            {
                ProcessMouse();
            }
#endif
            if (Input.touchCount > 0)
            {
                ProcessTouch(Input.GetTouch(0));
            }
        }

        private void ProcessMouse()
        {
            if (Input.GetMouseButtonDown(0))
            {
                BeginCapture(Input.mousePosition);
            }
            else if (Input.GetMouseButton(0) && _isCapturing)
            {
                ContinueCapture(Input.mousePosition);
            }
            else if (Input.GetMouseButtonUp(0) && _isCapturing)
            {
                EndCapture(Input.mousePosition);
            }
        }

        private void ProcessTouch(Touch touch)
        {
            switch (touch.phase)
            {
                case TouchPhase.Began:
                    BeginCapture(touch.position);
                    break;
                case TouchPhase.Moved:
                case TouchPhase.Stationary:
                    if (_isCapturing)
                    {
                        ContinueCapture(touch.position);
                    }
                    break;
                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (_isCapturing)
                    {
                        EndCapture(touch.position);
                    }
                    break;
            }
        }

        private void BeginCapture(Vector2 position)
        {
            _isCapturing = true;
            _currentSample.Reset(Time.time);
            _currentSample.AddPoint(position, Time.time);
        }

        private void ContinueCapture(Vector2 position)
        {
            _currentSample.AddPoint(position, Time.time);
        }

        private void EndCapture(Vector2 position)
        {
            _currentSample.AddPoint(position, Time.time);
            _isCapturing = false;

            if (IsTapSample(_currentSample))
            {
                if (Time.time - _lastTapTime <= doubleTapWindow && Vector2.Distance(position, _lastTapPosition) <= tapMaxDistance)
                {
                    var doubleTapSample = new GestureSample();
                    doubleTapSample.Reset(_lastTapTime);
                    doubleTapSample.AddPoint(_lastTapPosition, _lastTapTime);
                    doubleTapSample.AddPoint(position, Time.time);
                    OnGestureCompleted?.Invoke(doubleTapSample);
                    _lastTapTime = -10f;
                    _lastTapPosition = Vector2.zero;
                    return;
                }

                _lastTapTime = Time.time;
                _lastTapPosition = position;
            }

            OnGestureCompleted?.Invoke(_currentSample);
        }

        private bool IsTapSample(GestureSample sample)
        {
            return sample.Duration <= tapMaxDuration && sample.TotalDistance <= tapMaxDistance;
        }
    }
}
