using Godot;
using System;

public partial class QuitButton : Button
{
	public override void _Ready()
	{
		Pressed += () =>
		{
			GetTree().Paused = false;
			GetTree().Quit();
		};
	}
}
