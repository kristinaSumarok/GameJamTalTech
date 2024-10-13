using Godot;
using System;

public partial class Sewer : Node
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Timer>("Timer2").Timeout += () => {
			GetNode<SceneLoader>("/root/SceneLoader").changeToScene("First.tscn");
		};
	}

	
}
