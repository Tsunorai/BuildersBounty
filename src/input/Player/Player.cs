using System;
using Godot;

public partial class Player : CharacterBody3D
{
    [Signal]
    public delegate void InteractEventHandler();
    [Signal]
    public delegate void PlaceEventHandler();

    private const float SPEED = 5.0f;
    private const float JUMP_VELOCITY = 4f;
    private const float SENSITIVITY = 0.003f;

    Node3D m_Head;
    Camera3D m_Camera;

    public override void _Ready()
    {
        m_Head = GetNode<Node3D>("Head");
        m_Camera = m_Head.GetNode<Camera3D>("Camera");
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

    public override void _UnhandledInput(InputEvent @event)
    {
        if (@event is InputEventMouseMotion inputEvent)
        {
            m_Head.RotateY(-inputEvent.Relative.X * SENSITIVITY);
            m_Camera.RotateX(-inputEvent.Relative.Y * SENSITIVITY);

            Vector3 rotation = m_Camera.Rotation;
            rotation.X = Math.Clamp(m_Camera.Rotation.X, Mathf.DegToRad(-40), Mathf.DegToRad(60));
            m_Camera.Rotation = rotation;
        }
    }

    public override void _PhysicsProcess(double delta)
    {
        HandleMovement(delta);
        HandleInput();
        MoveAndSlide();
    }

    private void HandleInput()
    {
        if (Input.IsActionJustPressed("interact"))
        {
            EmitSignal(SignalName.Interact);
        }

        if (Input.IsActionJustPressed("place"))
        {
            EmitSignal(SignalName.Place);
        }
    }

    private void HandleMovement(double delta)
    {
        Vector3 velocity = Velocity;

        if (!IsOnFloor())
        {
            velocity += GetGravity() * (float)delta;
        }

        Vector2 inputDir = Input.GetVector("left", "right", "up", "down");
        Vector3 direction = (m_Head.Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
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

        if (Input.IsActionJustPressed("jump") && IsOnFloor())
        {
            velocity.Y = JUMP_VELOCITY;
        }

        Velocity = velocity;
    }
}
