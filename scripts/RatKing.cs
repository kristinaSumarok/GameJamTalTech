using Godot;
using System;

public partial class tunel : Area2D
{
	// Called when the node enters the scene tree for the first time.
	Label label_e_;
	CharacterBody2D player;
	
	public override void _Ready()
	{
		label_e_ = GetNode<Label>("use_E");
		label_e_.Visible = false;

		this.BodyEntered += OnBodyEntered;
		this.BodyExited += OnBodyExited;

		player=GetNode<CharacterBody2D>("/root/First/Doll");
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
			
		 }
	}
}
