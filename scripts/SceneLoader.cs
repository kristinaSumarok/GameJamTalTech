using Godot;
using System;
public partial class SceneLoader : Node
{


	public void changeToScene(string sceneName)
	{
		GetTree().ChangeSceneToFile($"res://scenes/{sceneName}");
	}
	
}
