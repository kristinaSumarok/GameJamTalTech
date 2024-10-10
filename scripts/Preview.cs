using Godot;
using System;

public partial class Preview : Node
{
	public override void _Ready()
	{
		GetNode<Timer>("Timer").Timeout += () => {
			GetNode<SceneLoader>("/root/SceneLoader").changeToScene("MainMenu.tscn");
		};
	}
}
