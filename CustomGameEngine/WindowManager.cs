using System;

namespace CustomGameEngine;

public static class WindowManager {

    private static Vector CameraPosition = new(0, 50);
    private static Vector WindowCenter;
    private static Vector WindowDimensions;
    private static readonly int Zoom = 9;

    public static Vector ToWindowCoords(Vector position) {
        Vector coords = WindowCenter - ((CameraPosition - position) * Zoom);
        coords.Y = WindowDimensions.Y - coords.Y; // Invert Y-axis to support Monogame's top to bottom rendering.
        return coords;
    }

    public static Vector ToWorldCoords(Vector position) {
        position.Y = WindowDimensions.Y - position.Y; // Invert Y-axis to support Monogame's top to bottom rendering.
        return CameraPosition + ((position - WindowCenter) / Zoom);
    }

    public static void UpdateWindowDimensions(int width, int height) {
        WindowDimensions = new(width, height);
        WindowCenter = new((int) Math.Floor(WindowDimensions.X / 2f), (int) Math.Floor(WindowDimensions.Y / 2f));
    }
}
