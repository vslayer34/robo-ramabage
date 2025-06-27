using Godot;
using RoboRampage.Helper.Input;
using System;

public partial class HitScanWeapon : Node3D
{
	[Export]
	private float fireRatePerSecond;

	[Export]
	private Timer _coolDownTimer;



	// Game Loop Methods---------------------------------------------------------------------------

	public override void _Process(double delta)
	{
		if (Input.IsActionPressed(InputActionName.FIRE))
		{
			if (_coolDownTimer.IsStopped())
			{
				_coolDownTimer.Start(1.0f / fireRatePerSecond);
				GD.Print("shot fired");
			}
		}
    }
}
