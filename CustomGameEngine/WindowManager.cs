using System;
using System.Numerics;

namespace CustomGameEngine;

public static class WindowManager {

    private static Vector2 CameraPosition = new(0, 50);
    private static Vector2 WindowDimensions;
    private static readonly int Zoom = 9;

    public static Vector ToWindowCoords(Vector position) {
        Vector2 center = new(WindowDimensions.X / 2f, WindowDimensions.Y / 2f);

        Vector2 coords = center - ((CameraPosition - new Vector2(position.X, position.Y)) * Zoom);
        coords.Y = WindowDimensions.Y - coords.Y; // Invert Y-axis to support Monogame's top to bottom rendering.

        return new((int) Math.Round(coords.X), (int) Math.Round(coords.Y));
    }

    public static Vector ToWorldCoords(Vector2 position) {
        Vector2 center = new(WindowDimensions.X / 2f, WindowDimensions.Y / 2f);

        position.Y = WindowDimensions.Y - position.Y; // Invert Y-axis to support Monogame's top to bottom rendering.
        Vector2 coords = CameraPosition + ((position - center) / Zoom);

        return new((int) Math.Round(coords.X), (int) Math.Round(coords.Y));
    }
}
