using Godot;
using RoboRamabage.Scripts.UI.GameOver;
using System;

public partial class LevelManager : Node3D
{
	public static LevelManager Instance { get; private set; }
	public event Action OnnPlayerDead;

	[Signal]
	public delegate void OnPlayerDeadEventHandler();

	[Export]
	private GameOverMenu _gameOverMenu;



	// Game Loop Methods---------------------------------------------------------------------------

	public override void _Ready()
	{
		Instance = this;
    }

	// Member Methods------------------------------------------------------------------------------

	public void StartPlayerDeadSequence()
	{
		OnnPlayerDead?.Invoke();
		Input.MouseMode = Input.MouseModeEnum.Visible;
	}
}
