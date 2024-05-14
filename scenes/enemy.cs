using Godot;
using System;

public partial class enemy : RigidBody2D
{
	
	[Export]
	public EnemyStats Stats;
	
	[Export] 
	public int Speed { get; set; } = 120;
	private Player player;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	
	if (Stats is EnemyStats enemyStats){
		enemyStats.damage(199);
		GD.Print(enemyStats.Health);
		GD.Print(enemyStats.Speed);
	}
	
		
		player = GetNode<Player>("/root/Main/Player");

		//stop from rotating
		Inertia = float.MaxValue;
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
	}

	public override void _IntegrateForces(PhysicsDirectBodyState2D state)
	{
		if(player is null) return;

		//find player and move in direction
		var direction = (player.Position - Position).Normalized();
		state.LinearVelocity = direction * Speed;
	}
}
