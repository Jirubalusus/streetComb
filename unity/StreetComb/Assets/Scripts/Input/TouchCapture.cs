using System;
using UnityEngine;

namespace StreetComb.InputSystem
{
    public class TouchCapture : MonoBehaviour
    {
        public event Action<GestureSample> OnGestureCompleted;

        [SerializeField] private bool useMouseInEditor = true;

        private readonly GestureSample _currentSample = new();
        private bool _isCapturing;

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
            OnGestureCompleted?.Invoke(_currentSample);
        }
    }
}
