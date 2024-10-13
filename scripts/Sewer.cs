using Godot;
using System;

public partial class Sewer : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	

	public override void _Ready()
	{
		GetNode<SceneLoader>("/root/SceneLoader").sewer = true;
		GetNode<Timer>("Timer2").Timeout += () => {
			GetNode<SceneLoader>("/root/SceneLoader").changeToScene("Second.tscn");
		};
 	
	}

	
}
