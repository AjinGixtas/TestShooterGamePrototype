using Godot;

public partial class Grenade : Bullet
{
	[Export] protected float DECCELERATION;
	public override void Intialize(Vector2 movementVector) { this.movementVector = movementVector; }
	public override void _Process(double delta)
	{
		if (SPEED > 0)
		{
			deltaF = (float)delta;
			SPEED -= DECCELERATION * deltaF;
			Position += movementVector * deltaF * SPEED;
		}
		else if (SPEED <= 0)
		{
			animationPlayer.Play("Collide");
		}
	}
}
