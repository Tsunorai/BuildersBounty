using Godot;

public partial class Block : CsgBox3D
{

    public void Interact()
    {
        Pickup();
    }

    public void Pickup()
	{
		QueueFree();
	}
}