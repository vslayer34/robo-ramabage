using Godot;
using System;


namespace RoboRamabage.Scripts.UI.GameOver;
public partial class RetryButton : Button
{
	public override void _Ready()
	{
		Pressed += () =>
		{
			GetTree().Paused = false;
			GD.Print(GetTree().CurrentScene.Name);
			GetTree().ReloadCurrentScene();
		};
	}
}
