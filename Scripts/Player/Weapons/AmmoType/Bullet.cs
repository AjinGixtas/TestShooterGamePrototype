using Godot;

public partial class Bullet : Node2D {
    protected Vector2 movementVector;
    protected float speed;
    [Export] protected float SPEED { get { return speed;} set { movementVector = movementVector * value / speed; speed = value; } }
    [Export] protected AnimationPlayer animationPlayer;
    protected float deltaF;
    public virtual void Intialize(Vector2 movementVector) {
        this.movementVector = movementVector * SPEED; 
    }
    public override void _Process(double delta) {
        Position += movementVector * (float)delta;
    }
    public virtual void OnAreaEntered(Area2D area2D) { PlaySelfDestructAnimation(); }
    public virtual void PlaySelfDestructAnimation() { animationPlayer.Play("Collide"); }
}