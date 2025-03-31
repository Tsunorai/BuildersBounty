using Godot;
using System;

public partial class InteractionManager : Node
{
    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        PlayerManager playerManager = GetNode<PlayerManager>("/root/PlayerManager");
        if (playerManager != null)
        {
            playerManager.Interact += OnInteract;
        }
    }

    private void OnInteract(Node3D interaction)
    {
        if (interaction is Block block)
        {
            block.Pickup();
        }
    }
}
