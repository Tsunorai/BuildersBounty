using Godot;

public partial class BlockManager : Node3D
{
    [Export] 
    public PackedScene m_BlockScene = ResourceLoader.Load<PackedScene>("res://src/core/interactables/Block.tscn");

    public override void _Ready()
    {
        PlayerManager playerManager = GetNode<PlayerManager>("/root/PlayerManager");
        if (playerManager != null)
        {
            playerManager.PlaceBlock += PlaceBlockAt;
        }
    }

    private void PlaceBlockAt(Vector3 position)
    {
        Node3D block = m_BlockScene.Instantiate() as Node3D;
        block.Position = position;
        AddChild(block);
    }
}
