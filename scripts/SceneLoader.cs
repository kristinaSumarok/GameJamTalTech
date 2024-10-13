using Godot;
using System;
public partial class SceneLoader : Node
{
	public bool talks = false;
	public int collectedLimbs = 0;

	public void changeToScene(string sceneName)
	{
		GetTree().ChangeSceneToFile($"res://scenes/{sceneName}");
	}
	
}
