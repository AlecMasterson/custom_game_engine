using System;
using System.Numerics;
using System.Threading;

namespace CustomGameEngine;

public class Voxel(int x, int y) {

    public readonly int Id = Interlocked.Increment(ref Logic.NextVoxelId);
    public int ParentId = -1;

    public Vector2 Acceleration = Vector2.Zero;
    public Vector2 Velocity = Vector2.Zero;

    public Vector Position = new(x, y);
    public Vector PositionNext = Vector.Zero;
    public Vector PositionPrev = Vector.Zero;

    public Vector2 PositionReal = new(x, y);
    public Vector2 PositionRealNext = Vector2.Zero;
    public Vector2 PositionRealPrev = Vector2.Zero;

    public int Mass = 1;

    public void UpdatePhysics(float delta) {
        Acceleration = new Vector2(0, Logic.Gravity) / Mass;
        Velocity += Acceleration * delta;
        PositionRealNext = PositionReal + (Velocity * delta);
        PositionNext = new Vector((int) Math.Round(PositionRealNext.X), (int) Math.Round(PositionRealNext.Y));
    }
}
