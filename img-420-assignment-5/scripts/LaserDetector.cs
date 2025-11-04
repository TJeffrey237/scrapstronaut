using Godot;
using System;

public partial class LaserDetector : Node2D
{
	[Export] public float LaserLength { get; set; } = 500f;
	[Export] public Color LaserColorNormal { get; set; } = Colors.Green;
	[Export] public Color LaserColorAlert { get; set; } = Colors.Red;
	[Export] public float LaserWidth { get; set; } = 3.0f;
	[Export] public NodePath PlayerPath { get; set; }
	[Export] public float AlarmDuration { get; set; } = 1.0f;

	private RayCast2D _rayCast;
	private Line2D _laserBeam;
	private CharacterBody2D _player;
	private bool _isAlarmActive = false;
	private Timer _alarmTimer;
	
	public override void _Ready()
	{
		SetupRaycast();
		SetupVisuals();
		
		// get player reference
		if(PlayerPath != null)
		{
			_player = GetNode<CharacterBody2D>(PlayerPath);
		}
		// setup alarm timer
		_alarmTimer = new Timer();
		_alarmTimer.OneShot = true;
		_alarmTimer.WaitTime = AlarmDuration;
		_alarmTimer.Timeout += ResetAlarm;
		AddChild(_alarmTimer);
	}
	
	private void SetupRaycast()
	{
		_rayCast = new RayCast2D();
		_rayCast.Name = "TheLaser";
		_rayCast.TargetPosition = new Vector2(LaserLength, 0);
		_rayCast.Enabled = true;
		_rayCast.CollisionMask = 1; 
		AddChild(_rayCast);
	}
	
	private void SetupVisuals()
	{
		_laserBeam = new Line2D();
		_laserBeam.Name = "LaserBeamVisuals";
		_laserBeam.DefaultColor = LaserColorNormal;
		_laserBeam.Width = LaserWidth;
		_laserBeam.AddPoint(Vector2.Zero);
		_laserBeam.AddPoint(new Vector2(LaserLength, 0)); 
		AddChild(_laserBeam);
	}
	
	public override void _PhysicsProcess(double delta)
	{
		_rayCast.ForceRaycastUpdate();
		
		if(_rayCast.IsColliding())
		{
			Node collider = (Node)_rayCast.GetCollider();
			if(collider != null && collider == _player)
			{
				if(!_isAlarmActive)
				{
					TriggerAlarm();
				}
			}
		}
		UpdateLaserBeam();
	}
	
	private void UpdateLaserBeam()
	{
		Vector2 hitPosition;
		
		if (_rayCast.IsColliding())
		{
			hitPosition = _rayCast.ToLocal(_rayCast.GetCollisionPoint());
			_laserBeam.DefaultColor = _isAlarmActive ? LaserColorAlert : LaserColorNormal;
		}
		else
		{
			hitPosition = _rayCast.TargetPosition;
			if (!_isAlarmActive)
			{
				_laserBeam.DefaultColor = LaserColorNormal;
			}
		}
		_laserBeam.SetPointPosition(1, hitPosition);
	}

	private void TriggerAlarm()
	{
		_isAlarmActive = true;
		_alarmTimer.Start();
		_laserBeam.DefaultColor = LaserColorAlert;
		GD.Print("Player Detected at: " + _rayCast.GetCollisionPoint());
	}

	private void ResetAlarm()
	{
		_isAlarmActive = false;
		_laserBeam.DefaultColor = LaserColorNormal;
	}
}
