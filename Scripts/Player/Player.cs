using Godot;
using System;
using RoboRampage.Helper.Input;


namespace RoboRampage.Player;
public partial class Player : CharacterBody3D
{
	[ExportCategory("Required Nodes")]
	[Export]
	private Node3D _cameraPivot;

	[ExportCategory("")]

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private Vector2 _mouseDirection;



    // Game Loop Methods---------------------------------------------------------------------------

    public override void _Ready()
    {
        Input.MouseMode = Input.MouseModeEnum.Captured;
    }

	public override void _PhysicsProcess(double delta)
	{
		HandleMouseRotation();

		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed(InputActionName.JUMP) && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 inputDir = Input.GetVector(InputActionName.MOVE_LEFT, InputActionName.MOVE_RIGHT, InputActionName.MOVE_FORWARD, InputActionName.MOVE_BACK);
		Vector3 direction = (Transform.Basis * new Vector3(inputDir.X, 0, inputDir.Y)).Normalized();
		
		if (direction != Vector3.Zero)
		{
			velocity.X = direction.X * Speed;
			velocity.Z = direction.Z * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

    public override void _Input(InputEvent @event)
    {
        if (@event is InputEventMouseMotion mouse && Input.MouseMode.Equals(Input.MouseModeEnum.Captured))
		{
			_mouseDirection = -mouse.Relative * 0.001f;
		}
		
		if (@event.IsActionPressed(InputActionName.ESCAPE) && Input.MouseMode.Equals(Input.MouseModeEnum.Captured))
		{
			Input.MouseMode = Input.MouseModeEnum.Visible;
		}
		else if (@event.IsActionPressed(InputActionName.ESCAPE) && Input.MouseMode.Equals(Input.MouseModeEnum.Visible))
		{
			Input.MouseMode = Input.MouseModeEnum.Captured;
		}
    }


	// Member Methods------------------------------------------------------------------------------

	private void HandleMouseRotation()
	{
		RotateY(_mouseDirection.X);
		_cameraPivot.RotateX(_mouseDirection.Y);
		_cameraPivot.RotationDegrees = _cameraPivot.RotationDegrees.Clamp(
			new Vector3(-90.0f, _cameraPivot.RotationDegrees.Y, _cameraPivot.RotationDegrees.Z),
			new Vector3(90.0f, _cameraPivot.RotationDegrees.Y, _cameraPivot.RotationDegrees.Z));
		_mouseDirection = Vector2.Zero;
	}
}
