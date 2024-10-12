using Godot;
using System;

public partial class MainMenu : ColorRect
{
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		GetNode<Button>("CenterContainer/VBoxContainer/Start").Pressed += () => {
			GetNode<SceneLoader>("/root/SceneLoader").changeToScene("Prologue.tscn");
		};
	}

	public void ExitGame() {
		GetTree().Quit();
	}
}
