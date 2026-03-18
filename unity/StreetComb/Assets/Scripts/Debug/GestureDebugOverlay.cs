using StreetComb.Fighters;
using StreetComb.InputSystem;
using UnityEngine;

namespace StreetComb.DebugTools
{
    public class GestureDebugOverlay : MonoBehaviour
    {
        [SerializeField] private TouchCapture touchCapture;
        [SerializeField] private FighterController playerFighter;
        [SerializeField] private bool drawTrail = true;

        private readonly GestureRecognizer _recognizer = new();
        private GestureResult _lastResult = GestureResult.None;
        private float _lastResultTime;

        private void Awake()
        {
            if (touchCapture != null)
            {
                touchCapture.OnGestureCompleted += HandleGestureCompleted;
            }
        }

        private void OnDestroy()
        {
            if (touchCapture != null)
            {
                touchCapture.OnGestureCompleted -= HandleGestureCompleted;
            }
        }

        private void HandleGestureCompleted(GestureSample sample)
        {
            _lastResult = _recognizer.Recognize(sample);
            _lastResultTime = Time.time;
            playerFighter?.ExecuteGesture(_lastResult.Type);
        }

        private void OnGUI()
        {
            GUILayout.BeginArea(new Rect(12, 12, 420, 220), GUI.skin.box);
            GUILayout.Label("StreetComb Gesture Debug");
            GUILayout.Label($"Last Gesture: {_lastResult.Type}");
            GUILayout.Label($"Confidence: {_lastResult.Confidence:0.00}");
            GUILayout.Label($"Age: {Time.time - _lastResultTime:0.00}s");
            if (playerFighter != null)
            {
                GUILayout.Label($"Player State: {playerFighter.CurrentState}");
                GUILayout.Label($"Player HP: {playerFighter.Health.CurrentHealth:0}/{playerFighter.Health.MaxHealth:0}");
                GUILayout.Label($"Player Energy: {playerFighter.Energy.CurrentEnergy:0}");
            }
            GUILayout.EndArea();
        }

        private void OnDrawGizmos()
        {
            if (!drawTrail || touchCapture == null || !touchCapture.IsCapturing)
            {
                return;
            }

            var sample = touchCapture.CurrentSample;
            if (sample.Points.Count < 2)
            {
                return;
            }

            Gizmos.color = Color.cyan;
            for (int i = 1; i < sample.Points.Count; i++)
            {
                Vector3 a = ScreenToWorld(sample.Points[i - 1]);
                Vector3 b = ScreenToWorld(sample.Points[i]);
                Gizmos.DrawLine(a, b);
            }
        }

        private Vector3 ScreenToWorld(Vector2 screenPoint)
        {
            Camera cam = Camera.main;
            if (cam == null)
            {
                return new Vector3(screenPoint.x * 0.01f, screenPoint.y * 0.01f, 0f);
            }

            Vector3 p = new(screenPoint.x, screenPoint.y, Mathf.Abs(cam.transform.position.z));
            return cam.ScreenToWorldPoint(p);
        }
    }
}
