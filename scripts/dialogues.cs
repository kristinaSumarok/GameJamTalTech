using DialogueManagerRuntime;
using Godot;
using Godot.Collections;
using System;

public partial class dialogues : Node
{	
	[Export] public Resource DialogueResource;
    [Export] public string DialogueStart = "start";

	public void Action(){
		DialogueManager.ShowDialogueBalloon(DialogueResource, DialogueStart);
	}
}