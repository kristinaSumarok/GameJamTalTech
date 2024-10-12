using DialogueManagerRuntime;
using Godot;
using System;

namespace helpers;
public partial class Actionable : Area2D
{
	[Export] public Resource DialogueResource;
	[Export] public string DialogueStart = "start";
	
	public void Action(){
		DialogueManager.ShowDialogueBalloon(DialogueResource, DialogueStart);
	}

}
