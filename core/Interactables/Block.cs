using Godot;
using System;

public partial class Block : CsgBox3D
{
	public void Pickup()
	{
		QueueFree();
	}
}