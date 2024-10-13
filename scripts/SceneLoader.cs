using Godot;
using System;
public partial class SceneLoader : Node
{
	public bool sewer = false;

	public void changeToScene(string sceneName, bool sewer = true)
	{
		GetTree().ChangeSceneToFile($"res://scenes/{sceneName}");

		if (sewer){
			GetNode<doll>($"res://scripts/doll.cs").Position = new Vector2(1000, 1000);
		}
	}
	
}
