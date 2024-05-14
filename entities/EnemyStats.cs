using Godot;
using System;

public partial class EnemyStats : Resource
{
	[Export]
	public int Speed { get; set; } = (int)GD.RandRange(50, 150);
	
	[Export]
	public int Health { get; set; }
	
	public int damage(int d){
		Health -= d;
		return Health;
	}
}
