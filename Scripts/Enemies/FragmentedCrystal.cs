using Godot;

public partial class FragmentedCrystal : Enemy
{
	bool canBeamBlast = true;
	enum BeamState { IDLE, TRACKING, ATTACK }
	[Export] BeamState currentBeamState;
	[Export] Timer BEAM_BLAST_COOLDOWN_TIMER; [Export] Node2D BEAM_BLAST_PIVOT; [Export] RayCast2D RAY_CAST;
	public override void _Process(double delta)
	{
		currentDistanceFromPlayerSquared = (GlobalPosition - PLAYER.Position).LengthSquared();
		if (currentDistanceFromPlayerSquared < ENGAGE_DISTANCE_SQUARED && currentState != CurrentState.ATTACK) { currentState = CurrentState.ENGAGE; }
		if (currentState == CurrentState.ENGAGE)
		{
			if (currentDistanceFromPlayerSquared < ATTACK_DISTANCE_SQUARED && canBeamBlast) Attack();
			else if (currentDistanceFromPlayerSquared > ATTACK_DISTANCE_SQUARED) {
				ANIMATION_PLAYER.Play("Move");
				Velocity = (PLAYER.Position - Position).Normalized() * currentSpeed;
			}
		}
		else if (currentState == CurrentState.ATTACK)
		{
			if (currentBeamState == BeamState.TRACKING) { BEAM_BLAST_PIVOT.LookAt(PLAYER.GlobalPosition); }
			else if (RAY_CAST.GetCollider() != null && currentBeamState == BeamState.ATTACK) { (RAY_CAST.GetCollider() as Hurtbox).TakingDamage(1); }
		}
		if (PLAYER.Position.X < Position.X) { sprite.Scale = new(-1, 1); } else { sprite.Scale = new(1, 1); }
		MoveAndSlide();
	}
	public override void Attack()
	{
		if (!canBeamBlast) return;
		canBeamBlast = false; currentState = CurrentState.ATTACK;
		BEAM_BLAST_COOLDOWN_TIMER.Start();
		ANIMATION_PLAYER.Play("Attack");
	}
	public void OnBeamAttackCooldownTimerTimeout()
	{
		canBeamBlast = true; currentState = CurrentState.ENGAGE;
	}
	public override void Move()
	{
		base.Move();
	}
	public override void Death()
	{
		SetProcess(false); ANIMATION_PLAYER.Play("Death");
	}
}
