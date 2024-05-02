using Godot;
using System;


public partial class Main : Node
{
	[Export]
	public PackedScene MobScene {get; set;}
	private int enemys;
	
	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		NewGame();
	}

	public override void _Process(double delta)
	{
		
	}
	
	public void NewGame()
	{
   		GetNode<Timer>("EnemyTimer").Start();
	}
	public void OnEnemyTimerTimeout()
{
	// Create a new instance of the Mob scene.
	enemy mob = MobScene.Instantiate<enemy>();

	// Choose a random location on Path2D.
	var mobSpawnLocation = GetNode<PathFollow2D>("Player/enemySpawner/PathFollow2D");
	mobSpawnLocation.ProgressRatio = GD.Randf();

	var camera = GetNode<Camera2D>("Player/Camera2D");

	// Set the mob's direction perpendicular to the path direction.
	float direction = mobSpawnLocation.Rotation + Mathf.Pi / 2;

	// spawing in center camera, need to spawn outside of camera
	var pos = camera.GetTargetPosition();
	var mobpos = mobSpawnLocation.Position;
	mobpos.X += pos.X;
	mobpos.Y += pos.Y;
	mob.Position = mobpos;

	// Add some randomness to the direction.
	direction += (float)GD.RandRange(-Mathf.Pi / 4, Mathf.Pi / 4);
	//mob.Rotation = direction;

	//// Choose the velocity.
	var velocity = new Vector2((float)GD.RandRange(150.0, 250.0), 0);
	mob.LinearVelocity = velocity.Rotated(direction);

	// Spawn the mob by adding it to the Main scene.
	AddChild(mob);
}
}
