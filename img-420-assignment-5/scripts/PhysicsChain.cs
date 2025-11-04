using Godot;
using System.Collections.Generic;

public partial class PhysicsChain : Node2D
{
	[Export] public int ChainSegments = 5;
	[Export] public float SegmentDistance = 30f;
	[Export] public PackedScene SegmentScene;
	[Export] public float JointSoftness = 0.5f;
	[Export] public float JointBias = 0.8f;
	
	private List<RigidBody2D> _segments = new List<RigidBody2D>();
	private List<Joint2D> _joints = new List<Joint2D>();
	
	private StaticBody2D _anchor;
	private const float SegmentLength = 20f;
	
	public override void _Ready()
	{
		CreateChain();
	}
	private void CreateChain()
	{
		RigidBody2D previousSegment = null;
		
		_anchor = new StaticBody2D();
		_anchor.Name = "ChainAnchor";
		
		var shape = new CollisionShape2D();
		shape.Shape = new RectangleShape2D() { Size = new Vector2(5, 5) };
		_anchor.AddChild(shape);
		AddChild(_anchor);

		for (int i = 0; i < ChainSegments; i++)
		{
			// Instantiate the segment
			var segment = SegmentScene.Instantiate<RigidBody2D>();
			segment.Name = $"Segment_{i}";
			
			// Set the first segment's collision layer so it doesn't collide with the anchor
			segment.CollisionLayer = (i == 0) ? (uint)1 : (uint)2; 
			
			// Place segments vertically, offsetting by the segment length to chain end-to-end
			float yPos = (i + 1) * SegmentDistance;
			segment.Position = new Vector2(0, yPos);
			
			AddChild(segment);
			_segments.Add(segment);

			// --- Connect segments with PinJoint2D ---
			var joint = new PinJoint2D();
			joint.Name = $"Joint_{i}";
			joint.Softness = JointSoftness; 
			joint.Bias = JointBias;
			
			if (previousSegment == null)
			{
				joint.Position = Vector2.Zero; 
				joint.NodeA = _anchor.GetPath(); 
				joint.NodeB = segment.GetPath();
			}
			else
			{
				joint.Position = previousSegment.Position + new Vector2(0, SegmentDistance / 2.0f);
				joint.NodeA = previousSegment.GetPath();
				joint.NodeB = segment.GetPath();
			}
			
			AddChild(joint);
			_joints.Add(joint);
			previousSegment = segment;
		}
	}
	public void ApplyForceToSegment(int segmentIndex, Vector2 force)
	{
		if (segmentIndex >= 0 && segmentIndex < _segments.Count)
		{
			var segment = _segments[segmentIndex];
			segment.ApplyCentralImpulse(force);
		}
	}
}
