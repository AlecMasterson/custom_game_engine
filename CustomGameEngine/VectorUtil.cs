using System;
using System.Numerics;

namespace CustomGameEngine;

public static class VectorUtil {

    public static Vector2 AdjustMagnitude(Vector2 vector, float delta) {
        if (vector == Vector2.Zero || delta == 0f) return vector;

        float magnitude = vector.Length();
        if (delta < 0 && MathF.Abs(delta) >= magnitude) return Vector2.Zero;

        return vector / magnitude * Math.Max(0f, magnitude + delta);
    }
}
