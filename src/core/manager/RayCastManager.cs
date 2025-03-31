using Godot;
using Godot.Collections;

public partial class RayCastManager : Node3D
{
    public static RayCastManager Instance;

    public override void _Ready()
    {
        Instance = this;
    }
    public static Dictionary CastRay(Vector3 from, Vector3 to, bool collideWithAreas = true, bool collideWithBodies = true)
    {
        GD.Print("worksnot ");
        PhysicsDirectSpaceState3D spaceState = Instance.GetWorld3D().DirectSpaceState;
        GD.Print("works");

        PhysicsRayQueryParameters3D query = new()
        {
            From = from,
            To = to,
            CollideWithAreas = collideWithAreas,
            CollideWithBodies = collideWithBodies
        };

        return spaceState.IntersectRay(query);
    }

    public static Node3D GetCollider(Dictionary rayResult)
    {
        if (rayResult.ContainsKey("collider"))
        {
            return (Node3D)rayResult["collider"];
        }
        return null;
    }

    public static Vector3 GetPosition(Dictionary rayResult)
    {
        if (rayResult.ContainsKey("position"))
        {
            return (Vector3)rayResult["position"];
        }
        return new();
    }

    public static Vector3 GetNormal(Dictionary rayResult)
    {
        if (rayResult.ContainsKey("normal"))
        {
            return (Vector3)rayResult["normal"];
        }
        return new();
    }
}
