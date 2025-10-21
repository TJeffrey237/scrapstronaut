using Godot;
using System;

public partial class HUD : CanvasLayer
{
	private Label _scoreLabel;
	
	public override void _Ready()
	{
		_scoreLabel = GetNode<Label>("ScoreLabel");
		updateScore(0);
	}
	
	public void updateScore(int newScore)
	{
		_scoreLabel.Text = $"Score: {newScore}";
	}
}
