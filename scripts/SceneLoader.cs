using Godot;
using System;

public partial class SceneLoader : Node
{
	public bool has_met_nathan = false;
	public void changeToScene(string sceneName)
	{
		GetTree().ChangeSceneToFile($"res://scenes/{sceneName}");
	}
}
