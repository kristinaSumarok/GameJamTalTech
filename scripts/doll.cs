using Godot;
using System;
using System.Drawing;

public partial class doll : CharacterBody2D
{
	public const float Speed = 300.0f;
	public const float JumpVelocity = -400.0f;

	public int collectedLimbs = 0;

    //Nodes
	private AnimatedSprite2D _standsprite;
	private AnimatedSprite2D _headMoveAnimation;
	private AnimatedSprite2D _withEarsSprite;
	private AnimatedSprite2D _walksNoHandsAnimation;
	private CollisionShape2D _collisionMask;

	public override void _Ready(){
		_standsprite = GetNode<AnimatedSprite2D>("NoEarsStatic");
		_headMoveAnimation = GetNode<AnimatedSprite2D>("HeadMove");
		_withEarsSprite = GetNode<AnimatedSprite2D>("withEarsStatic");
		_walksNoHandsAnimation = GetNode<AnimatedSprite2D>("WalksNoHands");
		_collisionMask = GetNode<CollisionShape2D>("CollisionShape2D");
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

		//TODO: replace with switch / case statement
		if (collectedLimbs == 0){
			_UpdateColisionMask(528.5, 221.5);
			if (!walking){
				_standsprite.Visible = true;
				_standsprite.Play();

				_headMoveAnimation.Visible = false;
				_headMoveAnimation.Stop();
			}
			else {
				_standsprite.Visible = false;
				_standsprite.Stop();

				_headMoveAnimation.Visible = true;
				_headMoveAnimation.Play();

				_headMoveAnimation.FlipH = velX < 0;
			}
		}
		else if (collectedLimbs == 1) {
			_standsprite.Stop();
			_withEarsSprite.Play();
		}
		else if (collectedLimbs == 2) {
			_UpdateColisionMask(528.5, 232);
		}
    }

	private void _UpdateColisionMask(double x, double y){
		_collisionMask.Position = new Vector2((float)x,(float)y);
	}
}
