using System.Collections.Generic;
using UnityEngine;

namespace StreetComb.InputSystem
{
    public class GestureSample
    {
        public readonly List<Vector2> Points = new();
        public float StartTime;
        public float EndTime;

        public float Duration => EndTime - StartTime;

        public Vector2 StartPoint => Points.Count > 0 ? Points[0] : Vector2.zero;
        public Vector2 EndPoint => Points.Count > 0 ? Points[Points.Count - 1] : Vector2.zero;

        public float TotalDistance
        {
            get
            {
                if (Points.Count < 2) return 0f;

                float distance = 0f;
                for (int i = 1; i < Points.Count; i++)
                {
                    distance += Vector2.Distance(Points[i - 1], Points[i]);
                }

                return distance;
            }
        }

        public Vector2 Delta => EndPoint - StartPoint;

        public void Reset(float time)
        {
            Points.Clear();
            StartTime = time;
            EndTime = time;
        }

        public void AddPoint(Vector2 point, float time)
        {
            if (Points.Count == 0 || Vector2.Distance(Points[Points.Count - 1], point) > 1f)
            {
                Points.Add(point);
            }

            EndTime = time;
        }
    }
}
