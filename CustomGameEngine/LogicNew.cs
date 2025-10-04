using System;
using System.Collections.Generic;
using System.Numerics;

namespace CustomGameEngine;

public static class LogicNew {

    public static int NextVoxelId = 0;
    public static readonly float AirResistance = 0.08f;
    public static readonly float Gravity = -9.81f;
    public static Dictionary<Vector, Voxel> Voxels = [];

    public static void Init() {
        Voxel temp1 = new(0, 45) {
            AtRest = true
        };
        Voxels[temp1.Position] = temp1;
        Voxel temp2 = new(0, 75);
        Voxels[temp2.Position] = temp2;
    }

    public static void Init_sphere() {
        Voxel parent = new(0, 50);
        Voxels[parent.Position] = parent;

        int temp = (int) Math.Floor(Shapes.Circle7.GetLength(0) / 2f);
        for (int y = -temp; y <= temp; y++) {
            for (int x = -temp; x <= temp; x++) {
                Vector position = new(parent.Position.X + x, parent.Position.Y + y);
                if (Shapes.Circle7[y + temp, x + temp] && parent.Position != position) {

                    Voxel child = new(x + parent.Position.X, y + parent.Position.Y) {
                        ParentId = parent.Id
                    };

                    Voxels[child.Position] = child;
                }
            }
        }
    }

    public static void CreateVoxel(int x, int y) {
        Voxel voxel = new(x, y);
        Voxels[voxel.Position] = voxel;
    }

    public static float GetFriction(Vector position) {
        if (Voxels.TryGetValue(position, out Voxel voxel)) {
            return voxel.Friction;
        }

        return 0f;
    }

    public static void Tick(float delta) {
        Dictionary<Vector, Voxel> VoxelsNext = [];

        List<int> completed = [];
        foreach (var kvp in Voxels) {
            kvp.Value.UpdatePhysics(delta);

            if (kvp.Value.PositionPrev == kvp.Value.Position || kvp.Value.AtRest) {
                VoxelsNext[kvp.Value.Position] = kvp.Value;
                completed.Add(kvp.Value.Id);
                continue;
            }
        }

        foreach (var kvp in Voxels) {
            if (completed.Contains(kvp.Value.Id)) continue;

            if (VoxelsNext.ContainsKey(kvp.Value.Position)) {
                if (kvp.Value.PositionPrev.X == kvp.Value.Position.X) {
                    if (kvp.Value.PositionPrev.Y < kvp.Value.Position.Y) {
                        // from the bottom
                        kvp.Value.Position += new Vector(0, -1);
                    } else {
                        // from the top
                        kvp.Value.Position += new Vector(0, 1);
                        kvp.Value.ChangeDirection(MathF.PI / 4, delta);
                    }
                } else if (kvp.Value.PositionPrev.Y == kvp.Value.Position.Y) {
                    if (kvp.Value.PositionPrev.X < kvp.Value.Position.X) {
                        // from the left
                        kvp.Value.Position += new Vector(-1, 0);
                    } else {
                        // from the right
                        kvp.Value.Position += new Vector(1, 0);
                    }
                } else if (kvp.Value.PositionPrev.X != kvp.Value.Position.X && kvp.Value.PositionPrev.Y != kvp.Value.Position.Y) {
                    // from a diagonal
                }

                kvp.Value.PositionReal = new Vector2(kvp.Value.Position.X, kvp.Value.Position.Y);
                VoxelsNext[kvp.Value.Position] = kvp.Value;
            } else {
                VoxelsNext[kvp.Value.Position] = kvp.Value;
            }
        }

        Voxels = new(VoxelsNext);
    }
}
