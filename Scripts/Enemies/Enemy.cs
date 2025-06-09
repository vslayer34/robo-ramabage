using Godot;
using RoboRamabage.Scripts.Helper.Groups;
using RoboRampage.Player;


namespace RoboRampage.Enemies;

public partial class Enemy : CharacterBody3D
{
	[Export]
	private NavigationAgent3D _navAgent;

	private Player.Player _playerNode;

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private bool _isTriggered;

	[Export]
	private float _aggroDistance = 12.0f;



    // Game Loop Methods---------------------------------------------------------------------------

	public override void _Ready()
	{
		_playerNode = GetTree().GetFirstNodeInGroup(GroupNames.PLAYER) as Player.Player;
	}

    public override void _Process(double delta)
    {
        _navAgent.TargetPosition = _playerNode.GlobalPosition;
    }


	public override void _PhysicsProcess(double delta)
	{
		_isTriggered = GlobalPosition.DistanceTo(_playerNode.GlobalPosition) <= _aggroDistance;

		var nextPosition = _navAgent.GetNextPathPosition();
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		Vector3 direction = _isTriggered ? GlobalPosition.DirectionTo(nextPosition) : Vector3.Zero;


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
}
