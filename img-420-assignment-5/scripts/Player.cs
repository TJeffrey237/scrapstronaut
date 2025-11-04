using Godot;
using System;

public partial class Player : CharacterBody2D
{
	[Export] public float Speed = 200f;

	public override void _Ready()
	{
		// nothing
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 input_vector = GetInputVector();
		
		// Normalize the vector to prevent faster diagonal movement
		// We only normalize if the player is actually pressing something
		if (input_vector.LengthSquared() > 0)
		{
			input_vector = input_vector.Normalized();
		}

		// 1. Calculate the new velocity
		// Velocity = Direction * Speed
		Velocity = input_vector * Speed;

		// 2. Move the character
		// MoveAndSlide automatically handles collision and moves the body along the walls.
		MoveAndSlide();
	}

	private Vector2 GetInputVector()
	{
		Vector2 input_vector = Vector2.Zero;
		
		// Gather input from the defined actions
		if (Input.IsActionPressed("ui_up"))
		{
			input_vector.Y -= 1;
		}
		if (Input.IsActionPressed("ui_down"))
		{
			input_vector.Y += 1;
		}
		if (Input.IsActionPressed("ui_left"))
		{
			input_vector.X -= 1;
		}
		if (Input.IsActionPressed("ui_right"))
		{
			input_vector.X += 1;
		}

		return input_vector;
	}
}
