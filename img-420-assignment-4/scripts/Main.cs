using Godot;
using System;

public partial class Main : Node2D
{
	private int _score = 0;
	private HUD _hud;
	private Player _player;
	
	public override void _Ready()
	{
		_hud = GetNode<HUD>("HUD");
		_player = GetNode<Player>("Player");
		var circuits = GetTree().GetNodesInGroup("circuits");
		// connecting signals
		_player.PlayerHit += OnPlayerHit;
		foreach (Circuit circuit in circuits)
		{
			circuit.CircuitCollected += OnCircuitCollected;
		}
	}
	
	private void OnPlayerHit()
	{
		// reset the score
		_score = 0;
		_hud.updateScore(_score);
	}
	
	private void OnCircuitCollected()
	{
		// add to score
		_score += 1;
		_hud.updateScore(_score);
	}
}
