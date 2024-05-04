using Godot;
using System;
using System.Reflection.Metadata;

public partial class Player : RigidBody2D
{
	
	[Export] 
	public int Speed { get; set; } = 400;
	
	public Vector2 ScreenSize;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		ScreenSize = GetViewportRect().Size;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		var velocity = Vector2.Zero;
		
		var action = GetPlayerAction();
		velocity = HandleMovement(action);

		var animatedSprite2D = GetNode<AnimatedSprite2D>("AnimatedSprite2D");

		if (velocity.Length() > 0)
		{
			velocity = velocity.Normalized() * Speed;
			animatedSprite2D.Play();
		}
		else
		{
			animatedSprite2D.Stop();
		}
		
		Position += velocity * (float)delta;
	}

	public static PlayerAction GetPlayerAction()
	{
		if (Input.IsActionPressed("move_right") && Input.IsActionPressed("move_up")) return PlayerAction.move_right_up;
		if (Input.IsActionPressed("move_right") && Input.IsActionPressed("move_down")) return PlayerAction.move_right_down;
		if (Input.IsActionPressed("move_left") && Input.IsActionPressed("move_up")) return PlayerAction.move_left_up;
		if (Input.IsActionPressed("move_left") && Input.IsActionPressed("move_down")) return PlayerAction.move_left_down;
		if (Input.IsActionPressed("move_right")) return PlayerAction.move_right;
		if (Input.IsActionPressed("move_left")) return PlayerAction.move_left;
		if (Input.IsActionPressed("move_down")) return PlayerAction.move_down;
		if (Input.IsActionPressed("move_up")) return PlayerAction.move_up;

		//if no input is pressed
		return PlayerAction.idle;	
	}

    public static Vector2 HandleMovement(PlayerAction action)
	{
		var velocity = Vector2.Zero;
        _ = action switch
        {
			PlayerAction.move_right_up => velocity.X += 1,
			PlayerAction.move_right_down => velocity.X += 1,
			PlayerAction.move_left_down => velocity.X -= 1,
			PlayerAction.move_left_up => velocity.X -= 1,
            PlayerAction.move_right => velocity.X += 1,
            PlayerAction.move_left => velocity.X -= 1,
            PlayerAction.move_down => velocity.Y += 1,
            PlayerAction.move_up => velocity.Y -= 1,
			PlayerAction.idle => 0,
            _ => 0
        };

		//handing movement along Y axis for diagonal action
		_ = action switch
        {
            PlayerAction.move_right_up => velocity.Y -= 1,
			PlayerAction.move_left_up => velocity.Y -= 1,
			PlayerAction.move_right_down => velocity.Y += 1,
			PlayerAction.move_left_down => velocity.Y += 1,
            _ => 0
        };

        return velocity;
	}

	public enum PlayerAction 
	{
		move_right,
		move_left,
		move_down,
		move_up,
		move_right_up,
		move_right_down,
		move_left_up, 
		move_left_down,
		idle
	}

}
