using Godot;
using System;

public partial class Rat : Node
{
	// Called when the node enters the scene tree for the first time.
	AnimatedSprite2D ratAnimation;
	public override void _Ready()
	{
		ratAnimation = GetNode<AnimatedSprite2D>("Actionable/ratMove");
		ratAnimation.Play();
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

	}
}
