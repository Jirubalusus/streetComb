using System.Collections.Generic;
using UnityEngine;

namespace StreetComb.InputSystem
{
    public class GestureRecognizer
    {
        private readonly float _tapMaxDuration = 0.18f;
        private readonly float _tapMaxDistance = 25f;
        private readonly float _holdMinDuration = 0.35f;
        private readonly float _holdMaxDistance = 30f;
        private readonly float _swipeMinDistance = 90f;
        private readonly float _uMinVertical = 120f;
        private readonly float _uMinHorizontalTravel = 35f;
        private readonly float _semiCircleMinDistance = 150f;

        public GestureResult Recognize(GestureSample sample)
        {
            if (sample == null || sample.Points.Count == 0)
            {
                return GestureResult.None;
            }

            float duration = sample.Duration;
            float totalDistance = sample.TotalDistance;
            Vector2 delta = sample.Delta;

            if (duration <= _tapMaxDuration && totalDistance <= _tapMaxDistance)
            {
                return new GestureResult(GestureType.Tap, 1f, Vector2.zero);
            }

            if (duration <= 0.28f && totalDistance <= _swipeMinDistance * 0.45f && sample.Points.Count == 2)
            {
                return new GestureResult(GestureType.DoubleTap, 0.98f, Vector2.zero);
            }

            if (duration >= _holdMinDuration && totalDistance <= _holdMaxDistance)
            {
                return new GestureResult(GestureType.Hold, 0.95f, Vector2.zero);
            }

            if (TryRecognizeU(sample.Points, out var uDirection))
            {
                return new GestureResult(GestureType.UGesture, 0.9f, uDirection);
            }

            if (TryRecognizeSemiCircle(sample.Points, totalDistance, out var semiDirection))
            {
                return new GestureResult(GestureType.SemiCircle, 0.85f, semiDirection);
            }

            if (delta.magnitude >= _swipeMinDistance)
            {
                Vector2 normalized = delta.normalized;

                if (Mathf.Abs(normalized.x) > Mathf.Abs(normalized.y))
                {
                    return normalized.x > 0
                        ? new GestureResult(GestureType.SwipeRight, 0.9f, Vector2.right)
                        : new GestureResult(GestureType.SwipeLeft, 0.9f, Vector2.left);
                }

                return normalized.y > 0
                    ? new GestureResult(GestureType.SwipeUp, 0.9f, Vector2.up)
                    : new GestureResult(GestureType.SwipeDown, 0.9f, Vector2.down);
            }

            return GestureResult.None;
        }

        private bool TryRecognizeU(List<Vector2> points, out Vector2 direction)
        {
            direction = Vector2.up;
            if (points.Count < 3) return false;

            int minYIndex = 0;
            float minY = points[0].y;
            for (int i = 1; i < points.Count; i++)
            {
                if (points[i].y < minY)
                {
                    minY = points[i].y;
                    minYIndex = i;
                }
            }

            if (minYIndex == 0 || minYIndex == points.Count - 1)
            {
                return false;
            }

            float startToDip = points[0].y - points[minYIndex].y;
            float dipToEnd = points[points.Count - 1].y - points[minYIndex].y;
            float horizontalTravel = Mathf.Abs(points[points.Count - 1].x - points[0].x);

            if (startToDip > 15f && dipToEnd >= _uMinVertical && horizontalTravel >= _uMinHorizontalTravel)
            {
                direction = (points[points.Count - 1] - points[minYIndex]).normalized;
                return true;
            }

            return false;
        }

        private bool TryRecognizeSemiCircle(List<Vector2> points, float totalDistance, out Vector2 direction)
        {
            direction = Vector2.right;
            if (points.Count < 4 || totalDistance < _semiCircleMinDistance)
            {
                return false;
            }

            Vector2 start = points[0];
            Vector2 end = points[points.Count - 1];
            Vector2 chord = end - start;
            if (chord.magnitude < 50f)
            {
                return false;
            }

            float maxDeviation = 0f;
            for (int i = 1; i < points.Count - 1; i++)
            {
                maxDeviation = Mathf.Max(maxDeviation, DistanceToLine(points[i], start, end));
            }

            if (maxDeviation < 40f)
            {
                return false;
            }

            direction = chord.normalized;
            return true;
        }

        private float DistanceToLine(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 line = lineEnd - lineStart;
            if (line.sqrMagnitude <= 0.001f)
            {
                return Vector2.Distance(point, lineStart);
            }

            float t = Vector2.Dot(point - lineStart, line) / line.sqrMagnitude;
            Vector2 projection = lineStart + line * Mathf.Clamp01(t);
            return Vector2.Distance(point, projection);
        }
    }
}
