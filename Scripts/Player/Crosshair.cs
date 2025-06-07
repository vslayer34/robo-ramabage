using Godot;
using System;




namespace RoboRampage.Player;
public partial class Crosshair : Control
{
    public override void _Draw()
    {
        DrawCircle(Vector2.Zero, 3.0f, Colors.DimGray);
        DrawCircle(Vector2.Zero, 2.0f, Colors.White);

        DrawLine(new Vector2(16.0f, 0.0f), new Vector2(32.0f, 0.0f), Colors.DimGray, 2.0f);
        DrawLine(new Vector2(16.0f, 0.0f), new Vector2(32.0f, 0.0f), Colors.White, 2.0f);

        DrawLine(new Vector2(-16.0f, 0.0f), new Vector2(-32.0f, 0.0f), Colors.DimGray, 2.0f);
        DrawLine(new Vector2(-16.0f, 0.0f), new Vector2(-32.0f, 0.0f), Colors.White, 2.0f);

        DrawLine(new Vector2(0.0f, 16.0f), new Vector2(0.0f, 32.0f), Colors.DimGray, 2.0f);
        DrawLine(new Vector2(0.0f, 16.0f), new Vector2(0.0f, 32.0f), Colors.White, 2.0f);

        DrawLine(new Vector2(0.0f, -16.0f), new Vector2(0.0f, -32.0f), Colors.DimGray, 2.0f);
        DrawLine(new Vector2(0.0f, -16.0f), new Vector2(0.0f, -32.0f), Colors.White, 2.0f);
    }
}