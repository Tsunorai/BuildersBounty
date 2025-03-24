using Godot;

public partial class Block : CsgBox3D, IInteractable
{
    [Export] public PackedScene BlockScene = ResourceLoader.Load<PackedScene>("res://core/interactables/Block.tscn");

    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
	{
		QueueFree();
	}

    public void Place(Vector3 placePosition, Node3D destination)
    {
        // Instantiate the block scene
        Block blockInstance = BlockScene.Instantiate<Block>();

        destination.GetParent().AddChild(blockInstance);
        blockInstance.GlobalTransform = new Transform3D(blockInstance.GlobalTransform.Basis, placePosition);
    }
}