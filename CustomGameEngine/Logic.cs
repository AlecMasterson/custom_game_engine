using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace CustomGameEngine;

public static class Logic {

    public static int NextVoxelId = 0;
    public static readonly float Gravity = -9;
    public static Dictionary<Vector, Voxel> Voxels = [];
    /*

    public static void Init() {
        Voxel parent = new(0, 50);
        Voxels[parent.Position] = parent;

        int temp = (int)Math.Floor(Shapes.Circle7.GetLength(0) / 2f);
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

        for (int x = -10; x <= 10; x++) {
            Voxel floor = new(x, 0);
            Voxels[floor.Position] = floor;
        }
    }

    public static void Tick(float delta) {
        Dictionary<Vector, List<Voxel>> VoxelsNext = [];

        foreach (var voxel in Voxels.Values) {
            voxel.UpdatePhysics(delta);

            if (VoxelsNext.TryGetValue(voxel.PositionNext, out List<Voxel> temp)) {
                temp.Add(voxel);
            } else {
                VoxelsNext[voxel.PositionNext] = [voxel];
            }
        }

        Dictionary<Vector, List<Voxel>> VoxelsResolved = [];
        while (VoxelsNext.Values.Any(temp => temp.Count > 1)) {
            VoxelsResolved = [];

            foreach (var kvp in VoxelsNext) {
                if (kvp.Value.Count > 1) {
                    foreach (var voxel in kvp.Value) {
                        voxel.PositionNext = voxel.Position;
                        voxel.PositionRealNext = voxel.PositionReal;
                        voxel.Velocity = Vector2.Zero;
                        if (VoxelsResolved.TryGetValue(voxel.PositionNext, out List<Voxel> temp)) {
                            temp.Add(voxel);
                        } else {
                            VoxelsResolved[voxel.PositionNext] = [voxel];
                        }
                    }
                } else {
                    VoxelsResolved[kvp.Key] = kvp.Value;
                }
            }

            int totalCountNext = VoxelsNext.Values.Sum(list => list.Count);
            int totalCountResolved = VoxelsResolved.Values.Sum(list => list.Count);
            if (totalCountNext != totalCountResolved) {
                foreach (var temp1 in VoxelsNext.Values) {
                    bool found = false;
                    foreach (var temp2 in VoxelsResolved.Values) {

                    }

                    //if (VoxelsResolved.ContainsKey(temp1.Key) && temp1.Value.Count != VoxelsResolved[temp1.Key].Count) {
                    //    throw new Exception("Lost voxels during collision resolution!");
                    //}
                }
                throw new Exception("Lost voxels during collision resolution!");
            }

            VoxelsNext = new Dictionary<Vector, List<Voxel>>(VoxelsResolved);
        }

        foreach (var kvp in VoxelsNext) {
            kvp.Value[0].PositionPrev = kvp.Value[0].Position;
            kvp.Value[0].PositionRealPrev = kvp.Value[0].PositionReal;
            kvp.Value[0].Position = kvp.Value[0].PositionNext;
            kvp.Value[0].PositionReal = kvp.Value[0].PositionRealNext;
        }

        Voxels = VoxelsNext.ToDictionary(kvp => kvp.Key, kvp => kvp.Value[0]);
    }
    */
}
