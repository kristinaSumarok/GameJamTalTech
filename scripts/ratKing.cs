using DialogueManagerRuntime;
using Godot;
using System;

public partial class ratKing : Area2D
{
	// Called when the node enters the scene tree for the first time.
	Label label_e_;
	public override void _Ready()
	{
		label_e_ = GetNode<Label>("use_E");
		label_e_.Visible = false;

		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	 private void OnBodyEntered(Node body)
		{
			label_e_.Visible = true;
		}

	private void OnBodyExited(Node body){
		 label_e_.Visible = false;
	}

	public override void _UnhandledInput(InputEvent @event){
		if(Input.IsActionJustPressed("ui_accept")){
			DialogueManager.ShowDialogueBalloon(GD.Load("res://dialogue/Rat.dialogue"), "start");
			if (GetOverlappingBodies().Count > 0)
            {
                var body = GetOverlappingBodies()[0];
                if (body is doll player)
                {
                    player._collectedlimbs += 1; // Update limbs after dialogue
                }
            }
		 }
	}
}
