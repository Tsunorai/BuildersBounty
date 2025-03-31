using Godot;

public partial class BlockManager : Node3D
{
    [Export]
    public PackedScene m_BlockScene;

    public override void _Ready()
    {
        PlayerManager playerManager = GetNode<PlayerManager>("/root/PlayerManager");
        if (playerManager != null)
        {
            playerManager.PlaceBlock += PlaceBlockAt;
        }
    }

    public override void _EnterTree()
    {
        if (m_BlockScene == null)
        {
            m_BlockScene = ResourceLoader.Load<PackedScene>("res://src/core/interactables/Block.tscn");
        }
    }

    private void PlaceBlockAt(Vector3 position)
    {
        Node3D block = m_BlockScene.Instantiate() as Node3D;
        block.Position = position;
        AddChild(block);
    }
}
