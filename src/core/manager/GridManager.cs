using Godot;
using System;

public partial class GridManager : Node
{
    private static float GRID_SIZE = 1.0f; 
    public static Vector3 AlignToGrid(Vector3 position)
    {
        return new Vector3
            (
                (float)Math.Round(position.X / GRID_SIZE) * GRID_SIZE,
                (float)Math.Round(position.Y / GRID_SIZE) * GRID_SIZE,
                (float)Math.Round(position.Z / GRID_SIZE) * GRID_SIZE
            );
    }

    public static Vector2 AlignToGrid(Vector2 position)
    {
        return new Vector2
            (
                (float)Math.Round(position.X / GRID_SIZE) * GRID_SIZE,
                (float)Math.Round(position.Y / GRID_SIZE) * GRID_SIZE
            );
    }
}
