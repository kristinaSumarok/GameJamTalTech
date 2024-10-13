
using System;
using Godot;
using Godot.Collections;
using helpers;


public partial class doll : CharacterBody2D
{
	public const float Speed = 500.0f;
	public const float JumpVelocity = -400.0f;
	SceneLoader _sceneLoader;
	public bool talks = false;
	


    //Nodes
	///TODO: replace with array of sprite animations if possible
	//static animations
	private AnimatedSprite2D _standsprite;
	private AnimatedSprite2D _withEarsSprite;
	private AnimatedSprite2D _withLegsSprite;
	private AnimatedSprite2D _withLegsArmsSprite;

	//moving animations
	private AnimatedSprite2D _walksWithHandsAnimation;
	private AnimatedSprite2D _walksNoHandsAnimation;
	private AnimatedSprite2D _headMoveAnimation;

	//jumping animations

	private AnimatedSprite2D _jumpWithHandsAnimation;

	//collision mask
	private CollisionShape2D _collisionMask;

	//dialogue manager
	Area2D actionableFinder;


	public override void _Ready(){
		//_sceneLoader = new SceneLoader();
		//talks = _sceneLoader.talks;
	
		_standsprite = GetNode<AnimatedSprite2D>("NoEarsStatic");
		_withEarsSprite = GetNode<AnimatedSprite2D>("withEarsStatic");
		_withLegsSprite = GetNode<AnimatedSprite2D>("withLegsStatic");
		_withLegsArmsSprite = GetNode<AnimatedSprite2D>("withLegsArmsStatic");

		_headMoveAnimation = GetNode<AnimatedSprite2D>("HeadMove");
		_walksNoHandsAnimation = GetNode<AnimatedSprite2D>("WalksNoHands");
		_walksWithHandsAnimation = GetNode<AnimatedSprite2D>("WalksWithHands");

		_jumpWithHandsAnimation = GetNode<AnimatedSprite2D>("JumpWithHands");

	 	_collisionMask = GetNode<CollisionShape2D>("CollisionShape2D");
		actionableFinder = GetNode<Area2D>("Direction/ActionableFinder");
		_sceneLoader = GetNode<SceneLoader>("/root/SceneLoader");

	}

	public override void _UnhandledInput(InputEvent @event){


		/*if (Input.IsActionJustPressed("ui_text_submit")){
			
			Array<Area2D> actionables = actionableFinder.GetOverlappingAreas();
			
			if(actionables.Count >0 && _sceneLoader.collectedLimbs == 0)
			{
				(actionables[0] as helpers.Actionable).Action();
	
				
			}
			
		}*/
	}


    public override void _PhysicsProcess(double delta)
	{
		Array<Area2D> actionables = actionableFinder.GetOverlappingAreas();
		if(actionables.Count >0 && _sceneLoader.collectedLimbs == 0)
			{
				_sceneLoader.talks = true;
				_sceneLoader.collectedLimbs = 1;
				(actionables[0] as helpers.Actionable).Action();
			}

		
        // Ensure tunel is not null before calling its methods
		if(!_sceneLoader.talks){
		Godot.Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
		{
			velocity += GetGravity() * (float)delta;
		}

		// Handle Jump.
		if (Input.IsActionJustPressed("ui_accept") && IsOnFloor()&& _sceneLoader.collectedLimbs != 0 && _sceneLoader.collectedLimbs != 1)
		{
			velocity.Y = JumpVelocity;
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		Godot.Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Godot.Vector2.Zero)
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
		 else{
		Velocity = Godot.Vector2.Zero;
			 _UpdateSpriteRenderer(0);
		 }
		}
		
			

	private void _UpdateSpriteRenderer(float velX)
    {
        bool walking = velX != 0;

		//TODO: replace with switch / case statement
		if (_sceneLoader.collectedLimbs == 0){
			_UpdateColisionMask(528.5f, 221.5f);
			_changeAnimationState(walking, _standsprite, _headMoveAnimation, velX);
		}
		else if (_sceneLoader.collectedLimbs == 1) {
			_standsprite.Visible = false;
			_standsprite.Stop();

			_changeAnimationState(walking, _withEarsSprite, _headMoveAnimation, velX);

		}
		else if (_sceneLoader.collectedLimbs == 2) {
			_withEarsSprite.Visible = false;
			_withEarsSprite.Stop();
			_headMoveAnimation.Visible = false;
			_headMoveAnimation.Stop();

			_UpdateColisionMask(528.5f, 232f);
			_changeAnimationState(walking, _withLegsSprite, _walksNoHandsAnimation, velX);
		}
		else if (_sceneLoader.collectedLimbs == 3) {
			_withLegsSprite.Visible = false;
			_withLegsSprite.Stop();
			_walksNoHandsAnimation.Visible = false;
			_walksNoHandsAnimation.Stop();

			_changeAnimationState(walking, _withLegsArmsSprite, _walksWithHandsAnimation, velX);
		}
    }

	private void _UpdateColisionMask(float x, float y){
		_collisionMask.Position = new Godot.Vector2(x,y);
	}

	private void _changeAnimationState(bool walking, AnimatedSprite2D _stand, AnimatedSprite2D _walks, float velX){

		if (!walking){
				_stand.Visible = true;
				_stand.Play();

				_walks.Visible = false;
				_walks.Stop();
			}
			else {
				_stand.Visible = false;
				_stand.Stop();

				_walks.Visible = true;
				_walks.Play();

				_walks.FlipH = velX < 0;
			}
	}

}
