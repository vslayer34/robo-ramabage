using Godot;
using RoboRampage.Helper.Input;
using System;

public partial class HitScanWeapon : Node3D
{
	[Export]
	private float fireRatePerSecond;

	[Export]
	private Timer _coolDownTimer;

	[Export]
	private Node3D _weaponMesh;

	[Export]
	private float _backwardRecoilForce = 0.05f;

	private Vector3 _defaultMeshPosition;

	[Export]
	private RayCast3D _rayCaster;



    // Game Loop Methods---------------------------------------------------------------------------

	public override void _Ready()
	{
		_defaultMeshPosition = _weaponMesh.Position;
	}

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(InputActionName.FIRE))
		{
			if (_coolDownTimer.IsStopped())
			{
				Shoot();
			}

			_weaponMesh.Position = _weaponMesh.Position.Lerp(_defaultMeshPosition, (float)delta * 10.0f);
		}
	}

	// Member Methods------------------------------------------------------------------------------

	private void Shoot()
	{
		_coolDownTimer.Start(1.0f / fireRatePerSecond);
		GD.Print("shot fired");
		GD.Print(_rayCaster.GetCollider());

		Vector3 currentWeaponPosition = _weaponMesh.Position;
		currentWeaponPosition.Z += _backwardRecoilForce;
		
		_weaponMesh.Position = currentWeaponPosition;
	}
}
