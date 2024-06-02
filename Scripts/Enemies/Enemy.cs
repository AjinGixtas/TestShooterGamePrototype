using Godot;

public partial class Enemy : CharacterBody2D {
    public static Player PLAYER;
    [Export] protected AnimationPlayer ANIMATION_PLAYER; [Export] protected Sprite2D sprite;
    [Export] protected float ENGAGE_DISTANCE_SQUARED, ATTACK_DISTANCE_SQUARED;
    [Export] protected float currentSpeed;
    protected float currentDistanceFromPlayerSquared;
    protected enum CurrentState { IDLE, ENGAGE, ATTACK }
    [Export] protected CurrentState currentState = CurrentState.IDLE;
    public virtual void Attack() { GD.Print("Attack() not implemented!"); }
    public virtual void Move() { GD.Print("Move() not implemented!"); }
    public virtual void Death() { GD.Print("Die() not implemented!"); }
}