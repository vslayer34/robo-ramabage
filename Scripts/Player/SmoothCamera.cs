using Godot;
using System;
using System.Threading.Tasks;

public partial class SmoothCamera : Camera3D
{
	[Export]
	private float _smoothSpeed = 44.0f;

	private Node3D _cameraPivot;



    // Game Loop Methods---------------------------------------------------------------------------

    public override async void _Ready()
    {
        await ToSignal(Owner, SignalName.Ready);
		_cameraPivot = GetParent<Node3D>();
    }

    public override void _PhysicsProcess(double delta)
    {
        float weight = (float)delta * _smoothSpeed;

		// Set Position and Rotation
		GlobalTransform = GlobalTransform.InterpolateWith(_cameraPivot.GlobalTransform, weight);

		// Reset position
		GlobalPosition = _cameraPivot.GlobalPosition;
    }
}
