using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private Label _scoreLabel;
	
	public override void _Ready()
	{
		// initialize score label
		_scoreLabel = GetNode<Label>("ScoreLabel");
		updateScore(0);
	}
	
	public void updateScore(int newScore)
	{
		// update score label
		_scoreLabel.Text = $"Score: {newScore}";
	}
}
