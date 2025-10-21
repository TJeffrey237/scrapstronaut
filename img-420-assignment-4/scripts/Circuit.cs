using Godot;
using System;

public partial class Circuit : Area2D
{
	[Signal] public delegate void CircuitCollectedEventHandler();
	[Export] public float pulseSpeed;
	[Export] public float pulseAmplitude;
	private GpuParticles2D _emitter;
	private CollisionShape2D _collision;
	private Node2D _visualNode;
	private Vector2 baseScale;
	private float timePassed;

	public override void _Ready()
	{
		// get the relevant nodes
		_emitter = GetNode<GpuParticles2D>("bolts");
		_collision = GetNode<CollisionShape2D>("CollisionShape2D");
		_visualNode = GetNode<Node2D>("Sprite2D");
		baseScale = _visualNode.Scale;
		_emitter.Finished += OnEmitterFinished;
	}
	
	public override void _Process(double delta)
	{
		// for the pulsating sprite of the circuit
		timePassed += (float)delta;
		float currentPulse = 1.0f + (Mathf.Sin(timePassed * pulseSpeed) * pulseAmplitude);
		if (_visualNode != null)
		{
			_visualNode.Scale = baseScale * currentPulse;
		}
	}

	public void _on_body_entered(CharacterBody2D body)
	{
		// check for player collision
		if (body.IsInGroup("player") && !_collision.Disabled)
		{
			_collision.SetDeferred("disabled", true);
			// hide the circuit
			if (_visualNode != null)
			{
				_visualNode.Visible = false;
			}
			// play particles
			_emitter.Emitting = true;
			EmitSignal(SignalName.CircuitCollected);
		}
	}

	private void OnEmitterFinished()
	{
		// free circuit after particles play
		QueueFree();
	}
}
