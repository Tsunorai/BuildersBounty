using Godot;
using System;

public interface IInteractable
{
    void Interact();
    void Place(Vector3 placePosition, Node3D destination);
}
