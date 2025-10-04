using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading;

namespace CustomGameEngine;

public class Voxel(int x, int y) {

    public readonly int Id = Interlocked.Increment(ref Logic.NextVoxelId);
    public int ParentId = -1;

    public Vector2 Acceleration = Vector2.Zero;
    public Vector2 AccelerationPrev = Vector2.Zero;

    public Vector2 Velocity = Vector2.Zero;
    public Vector2 VelocityPrev = Vector2.Zero;

    public Vector Position = new(x, y);
    public Vector PositionPrev = Vector.Zero;

    public Vector2 PositionReal = new(x, y);
    public Vector2 PositionRealPrev = Vector2.Zero;

    public bool AtRest = false;
    public float Friction = 0.65f;
    public int Mass = 1;

    public void ChangeDirection(float deg, float delta) {
        float cosA = MathF.Cos(deg);
        float sinA = MathF.Sin(deg);

        float x = (Velocity.X * cosA) - (Velocity.Y * sinA);
        float y = (Velocity.X * sinA) + (Velocity.Y * cosA);

        Velocity = new(x, y);

        PositionReal = PositionRealPrev + (Velocity * delta);
        Position = new((int) Math.Round(PositionReal.X), (int) Math.Round(PositionReal.Y));
    }

    public void UpdatePhysics(float delta) {
        if (AtRest) {
            Acceleration = Vector2.Zero;
            Velocity = Vector2.Zero;
            return;
        }

        AccelerationPrev = Acceleration;
        Acceleration = new Vector2(0, Logic.Gravity) / Mass;

        VelocityPrev = Velocity;
        Velocity += Acceleration * delta;
        Velocity = VectorUtil.AdjustMagnitude(Velocity, GetTotalFriction() * delta);

        PositionRealPrev = PositionReal;
        PositionReal += Velocity * delta;

        PositionPrev = Position;
        Position = new Vector((int) Math.Round(PositionReal.X), (int) Math.Round(PositionReal.Y));
    }

    private float GetTotalFriction() {
        float friction = LogicNew.AirResistance;

        if (MathF.Abs(Velocity.X) > MathF.Abs(Velocity.Y)) {
            friction += LogicNew.GetFriction(Position + new Vector(0, -1));
            friction += LogicNew.GetFriction(Position + new Vector(0, 1));
        }
        if (MathF.Abs(Velocity.Y) > MathF.Abs(Velocity.X)) {
            friction += LogicNew.GetFriction(Position + new Vector(-1, 0));
            friction += LogicNew.GetFriction(Position + new Vector(1, 0));
        }

        return -friction;
    }
}
