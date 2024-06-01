using Godot;

public partial class RobotDuelist : Enemy {
	bool canSlash = true;
	[Export] Timer SLASH_COOLDOWN_TIMER;
	public override void _Process(double delta) {
		currentDistanceFromPlayerSquared = (GlobalPosition - PLAYER.Position).LengthSquared();
        GD.Print(currentDistanceFromPlayerSquared, ' ', Velocity, ' ', currentState);
		if(currentDistanceFromPlayerSquared < ENGAGE_DISTANCE_SQUARED) { currentState = CurrentState.ENGAGE; }
		if(currentState == CurrentState.ENGAGE) { 
			if(currentDistanceFromPlayerSquared < ATTACK_DISTANCE_SQUARED) Attack(); 
		}
		Velocity = (PLAYER.Position - Position).Normalized() * currentSpeed;
		MoveAndSlide();
	}
	public override void Attack() {
		if(!canSlash) return;
		currentState = CurrentState.ATTACK;
		SLASH_COOLDOWN_TIMER.Start();
		if(PLAYER.Position.Y < Position.Y) ANIMATION_PLAYER.Play("SlashUpward");
		else ANIMATION_PLAYER.Play("SlashDownward");
	}
	public void OnSlashCooldownTimerTimeout() {
		canSlash = true;
	}
}
