using System;

namespace CustomGameEngine;

public struct Vector(int x, int y) {

    public int X = x;
    public int Y = y;

    public static Vector operator +(Vector a, Vector b) => new(a.X + b.X, a.Y + b.Y);
    public static Vector operator -(Vector a, Vector b) => new(a.X - b.X, a.Y - b.Y);
    public static Vector operator *(Vector a, int scalar) => new(a.X * scalar, a.Y * scalar);
    public static bool operator ==(Vector a, Vector b) => a.X == b.X && a.Y == b.Y;
    public static bool operator !=(Vector a, Vector b) => !(a == b);
    public static Vector Zero => new(0, 0);
    public override bool Equals(object obj) => obj is Vector other && this == other;
    public override int GetHashCode() => HashCode.Combine(X, Y);
    public override string ToString() => $"({X}, {Y})";
}
