using Godot;
using RoboRamabage.Scripts.Helper.Animations;
using RoboRamabage.Scripts.Helper.Groups;
using RoboRampage.Player;


namespace RoboRampage.Enemies;

public partial class Enemy : CharacterBody3D
{
	[Export]
	private NavigationAgent3D _navAgent;

	[Export]
	private AnimationPlayer _animePlayer;

	private Player.Player _playerNode;

	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

	private bool _isTriggered;

	[Export]
	private float _aggroDistance = 12.0f;

	[Export]
	private float _attackRange = 1.5f;



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
		var _distanceToPlayer = GlobalPosition.DistanceTo(_playerNode.GlobalPosition);

		_isTriggered = _distanceToPlayer <= _aggroDistance;


		var nextPosition = _navAgent.GetNextPathPosition();
		Vector3 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		Vector3 direction = _isTriggered ? GlobalPosition.DirectionTo(nextPosition) : Vector3.Zero;

		if (_isTriggered && _distanceToPlayer <= _attackRange)
		{
			_animePlayer.Play(AnimationNames.Enemy.ATTACK);
		}


		if (direction != Vector3.Zero)
		{
			LookTowardDestination(direction);
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


	private void LookTowardDestination(Vector3 direction)
	{
		var directionWithoutYRotation = direction;
		directionWithoutYRotation.Y = 0.0f;

		// Added the global position because the direction is only local position

		LookAt(GlobalPosition + directionWithoutYRotation, Vector3.Up, true);
	}

	private void Attack()
	{
		GD.Print("Attack Player");
	}
}
