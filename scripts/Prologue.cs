using Godot;
using System;

public partial class Prologue : Node
{
	public override void _Ready()
	{
		GetNode<Timer>("Timer1").Timeout += () => {
			GetNode<SceneLoader>("/root/SceneLoader").changeToScene("First.tscn");
		};
	}
}
