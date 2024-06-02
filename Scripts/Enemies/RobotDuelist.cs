using Godot;

public partial class RobotDuelist : Enemy {
	bool canSlash = true;
	[Export] Timer SLASH_COOLDOWN_TIMER; [Export] Node2D SLASH_ATTACK_CONTAINER, SLASH_ATTACK_PIVOT;
	public override void _Process(double delta) {
		currentDistanceFromPlayerSquared = (GlobalPosition - PLAYER.Position).LengthSquared();
		if(currentDistanceFromPlayerSquared < ENGAGE_DISTANCE_SQUARED && currentState != CurrentState.ATTACK) { currentState = CurrentState.ENGAGE; }
		if(currentState == CurrentState.ENGAGE) { 
			if(currentDistanceFromPlayerSquared < ATTACK_DISTANCE_SQUARED) Attack();
			else ANIMATION_PLAYER.Play("Move");
		}
		if(PLAYER.Position.X > Position.X) { sprite.Scale = new(-1, 1); SLASH_ATTACK_CONTAINER.Scale = new(1, -1); } else { sprite.Scale = new(1, 1); SLASH_ATTACK_CONTAINER.Scale = new(1, 1); }
		SLASH_ATTACK_PIVOT.LookAt(PLAYER.GlobalPosition);
		Move();
	}
	public override void Attack() {
		if(!canSlash) return;
		currentState = CurrentState.ATTACK; canSlash = false;
		SLASH_COOLDOWN_TIMER.Start();
		if(PLAYER.Position.Y + 16 < Position.Y) ANIMATION_PLAYER.Play("SlashUpward");
		else ANIMATION_PLAYER.Play("SlashDownward");
	}
	public void OnSlashCooldownTimerTimeout() {
		canSlash = true; currentState = CurrentState.ENGAGE;
	}
	public override void Move() {
		Velocity = (PLAYER.Position - Position).Normalized() * currentSpeed;
		MoveAndSlide();
	} 
    public override void Death()
    {
		SetProcess(false); ANIMATION_PLAYER.Play("Death");
    }
}
