using Godot;
using System;

public partial class player : CharacterBody2D
{
	[Export] public float speed = 180.0f;
	[Export] public float jumpVelocity = -300.0f;
	[Export] public float doubleJumpVelocity = -260.0f;

	public AnimatedSprite2D _AnimatedSprite2D;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();
	public bool	hasDoubleJump = false;
	public bool animationLocked = false;
    public Vector2 direction = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;
		else
			hasDoubleJump = false;

		// Handle Jump.
		if (Input.IsActionJustPressed("jump"))
		{
			if (IsOnFloor())
				velocity.Y = jumpVelocity;
			else if(hasDoubleJump == false)
			{
				velocity.Y = doubleJumpVelocity;
				hasDoubleJump = true;
			}
		}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		 direction = Input.GetVector("left", "right", "up", "down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}
}
