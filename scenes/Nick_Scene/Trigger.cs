using Godot;
using System;

public partial class Trigger : Node2D // or Control
{
	private HSlider hSlider;
	private Label feedbackLabel;
	private Label riddleLabel;
	private HBoxContainer answerContainer;
	private float speed = 150f; // Speed of the slider movement
	private bool movingRight = true; // Direction of the slider movement
	private float targetZoneMin = 0.4f; // Minimum value of the target zone
	private float targetZoneMax = 0.6f; // Maximum value of the target zone
	private string[] answers = { "Hands", "Legs", "Rollerblades", "Map", "Shoes" }; // "Legs" is now in the 2nd position
	private int correctAnswerIndex = 1; // Update to match the new position of "Legs"
	private int selectedAnswerIndex = 0; // Track the currently selected answer index

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		hSlider = GetNode<HSlider>("/root/Sliders/HSliderRiddle1");
		feedbackLabel = GetNode<Label>("/root/Sliders/FeedBackLabel");
		riddleLabel = GetNode<Label>("/root/Sliders/RiddleLabel");
		answerContainer = GetNode<HBoxContainer>("/root/Sliders/AnswerContainer");
		
		hSlider.TickCount = answers.Length + 1;
		hSlider.TicksOnBorders = false;

		for (int i = 0; i < answers.Length; i++)
		{
			Label answerLabel = new Label();
			answerLabel.Text = answers[i];
			answerLabel.HorizontalAlignment = HorizontalAlignment.Center;
			answerLabel.CustomMinimumSize = new Vector2(hSlider.Size.X / answers.Length, 0);
			answerContainer.AddChild(answerLabel);
		}

		// Position the answer container
		answerContainer.Position = new Vector2(hSlider.Position.X, hSlider.Position.Y + hSlider.Size.Y + 10);
	}

	public override void _Process(double delta)
	{
		MoveSlider(delta);
		CheckForKeyPress();
	}

	private void MoveSlider(double delta)
	{
		double newValue = hSlider.Value + (movingRight ? speed : -speed) * delta;
		if (newValue >= hSlider.MaxValue)
		{
			newValue = hSlider.MaxValue;
			movingRight = false;
		}
		else if (newValue <= hSlider.MinValue)
		{
			newValue = hSlider.MinValue;
			movingRight = true;
		}

		// Update the selected answer index based on the slider's position
		selectedAnswerIndex = (int)(hSlider.Value / hSlider.MaxValue * answers.Length);

		hSlider.Value = newValue;
	}

	private void CheckForKeyPress()
	{
		if (Input.IsActionJustPressed("ui_accept")) // "ui_accept" is usually mapped to the "E" key
		{
			if (selectedAnswerIndex == correctAnswerIndex)
			{
				feedbackLabel.Text = "Nice!";
				speed = 0; // Stop the slider
			}
			else
			{
				feedbackLabel.Text = "";
				speed += 20f; // Increase speed if the player misses the target zone
				if (speed > 250f)
				{
					speed = 250f; // Cap the speed at 250f
				}
			}
		}
	}
}
