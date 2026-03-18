using UnityEngine;

namespace StreetComb.InputSystem
{
    public readonly struct GestureResult
    {
        public readonly GestureType Type;
        public readonly float Confidence;
        public readonly Vector2 Direction;

        public GestureResult(GestureType type, float confidence, Vector2 direction)
        {
            Type = type;
            Confidence = confidence;
            Direction = direction;
        }

        public static GestureResult None => new(GestureType.None, 0f, Vector2.zero);
    }
}
