using Godot;
using System;
using System.Threading.Tasks;

namespace RoboRamabage.Scripts.UI.GameOver;

public partial class GameOverMenu : Control
{
	public override async void _Ready()
	{
		Visible = false;

		await ToSignal(Owner, SignalName.Ready);

		LevelManager.Instance.OnnPlayerDead += ShowGameOver;
	}


    public override void _ExitTree()
	{
		LevelManager.Instance.OnnPlayerDead -= ShowGameOver;
	}



	public void ShowGameOver()
	{
		Visible = true;
		GetTree().Paused = true;
	}
}
