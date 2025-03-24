using Godot;
using Godot.Collections;

public partial class InteractionManager : Node3D
{
    public static InteractionManager Instance;

    // Reference to the player (set this manually or find it at runtime)
    public Player m_Player { get; set; }


    public override void _Ready()
    {
        Instance = this;
        // Optionally, auto-find the player in the current scene if not set manually.
        if (m_Player == null)
        {
            m_Player = GetTree().CurrentScene.FindChild("Player", true, false) as Player;
            if (m_Player != null)
            {
                GD.Print("Player found by InteractionManager.");
            }
            else
            {
                GD.Print("Player not found! Please assign the Player reference.");
                GetTree().Quit();
            }
        }
    }

    public void Place()
    {
        Dictionary result = CastRay();
        if (result.Count > 0)
        {
            Vector3 collisionPoint = (Vector3)result["position"];
            Vector3 collisionNormal = (Vector3)result["normal"];
            GodotObject collider = (GodotObject)result["collider"];

            // Offset position to place block next to the surface.
            Vector3 placePosition = collisionPoint + collisionNormal * 0.5f;

            Block block = new();
            block.Place(placePosition, (Node3D)collider);
            
        }
    }

    public void Interact()
    {
        Dictionary result = CastRay();

        if (result.Count > 0)
        {
            GodotObject collider = (GodotObject)result["collider"];
            if (collider is IInteractable interactable)
            {
                interactable.Interact();
            }
            else
            {
                GD.Print("InteractionManager: Object found but it is not interactable.");
            }
        }
    }

    public Dictionary CastRay()
    {
        Camera3D camera = m_Player.GetNode<Camera3D>("Head/Camera");
        // Perform a raycast query from the player's global position.
        PhysicsDirectSpaceState3D spaceState = GetWorld3D().DirectSpaceState;
        Vector3 from = camera.GlobalPosition;

        // Assume the player's forward direction is its -Z axis; adjust as needed.
        Vector3 to = from + camera.GlobalTransform.Basis.Z * -3.0f;  // 3 units ahead

        PhysicsRayQueryParameters3D query = new PhysicsRayQueryParameters3D
        {
            From = from,
            To = to,
            CollideWithAreas = true,
            CollideWithBodies = true
        };

        return spaceState.IntersectRay(query);
    }
}
