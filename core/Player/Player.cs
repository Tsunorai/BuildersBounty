using Godot;
using System;

public partial class Player : CharacterBody3D
{
    public const float SPEED = 5.0f;
    public const float JUMP_VELOCITY = 4f;
    public const float SENSITIVITY = 0.003f;


    Node3D Head;
    Camera3D Camera;

    public override void _Ready()
    {
        Head = GetNode<Node3D>("Head");
        Camera = Head.GetNode<Camera3D>("Camera");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion inputEvent)
        {
            Head.RotateY(-inputEvent.Relative.X * SENSITIVITY);
            Camera.RotateX(-inputEvent.Relative.Y * SENSITIVITY);

            Vector3 rotation = Camera.Rotation;
            rotation.X = Math.Clamp(Camera.Rotation.X, Mathf.DegToRad(-40), Mathf.DegToRad(60));
            Camera.Rotation = rotation;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        Vector3 velocity = Velocity;

        // Add the gravity.
        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        // Handle Jump.
        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JUMP_VELOCITY;
        }

        // Get the input direction and handle the movement/deceleration.
        // As good practice, you should replace UI actions with custom gameplay actions.
        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector3 direction = (Head.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
        if (direction != Vector3.Zero)
        {
            velocity.X = direction.X * SPEED;
            velocity.Z = direction.Z * SPEED;
        }
        else
        {
            velocity.X = 0.0f;
            velocity.Z = 0.0f;

        }

        Velocity = velocity;
        MoveAndSlide();
    }
}
