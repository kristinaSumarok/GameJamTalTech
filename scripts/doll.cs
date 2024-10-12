using Godot;
using System;

public partial class doll : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public int collectedLimbs = 0;


	private AnimatedSprite2D _standsprite;
	private AnimatedSprite2D _withEarsSprite;

	public override void _Ready(){
		_standsprite = GetNode<AnimatedSprite2D>("NoEarsStatic");
		_withEarsSprite = GetNode<AnimatedSprite2D>("withEarsStatic");
	}

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor())
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;

		_UpdateSpriteRenderer(velocity.X);
		MoveAndSlide();
	}

	private void _UpdateSpriteRenderer(float velX)
    {
        bool walking = velX != 0;

		//TODO: replace with case statement
		if (collectedLimbs == 0){

			if (!walking){
				_standsprite.Play();
			}
			else {
				
			}
		}
		else {
			_standsprite.Stop();
			_withEarsSprite.Play();
		}
	
    }
}
