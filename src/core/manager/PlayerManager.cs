using Godot;
using Godot.Collections;

public partial class PlayerManager : Node
{
    [Signal]
    public delegate void PlaceBlockEventHandler(Vector3 position);

    private Player m_Player;
    private Camera3D m_Camera;

    private const float REACH = 4.0f;

    public override void _Ready()
    {
        m_Player = GetNode<Player>("/root/Main/Player");
        m_Camera = m_Player.GetNode<Camera3D>("Head/Camera");
        if (m_Player != null)
        {
            m_Player.Place += OnPlace;
            m_Player.Interact += OnInteract;
        }
    }

    private void OnInteract()
    {
        Vector3 from = m_Camera.GlobalTransform.Origin;
        Vector3 to = from - m_Camera.GlobalTransform.Basis.Z * REACH;
        Dictionary result = RayCastManager.CastRay(from, to);

        Node3D collider = RayCastManager.GetCollider(result);

        EmitSignal(SignalName.PlaceBlock, collider);
    }

    private void OnPlace()
    {
        Vector3 from = m_Camera.GlobalTransform.Origin;
        Vector3 to = from - m_Camera.GlobalTransform.Basis.Z * REACH;
        Dictionary result = RayCastManager.CastRay(from , to);

        Vector3 blockPosition = RayCastManager.GetPosition(result) + RayCastManager.GetNormal(result) * 0.5f;
        Vector3 gridPosition = GridManager.AlignToGrid(blockPosition);

        EmitSignal(SignalName.PlaceBlock, gridPosition);
    }
}
