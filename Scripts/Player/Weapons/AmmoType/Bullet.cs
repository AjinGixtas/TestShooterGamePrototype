using Godot;

public partial class Bullet : Node2D {
    protected Vector2 movementVector;
    [Export] protected float SPEED;
    [Export] protected AnimationPlayer animationPlayer;
    protected float deltaF;
    public virtual void Intialize(Vector2 movementVector) {
        this.movementVector = movementVector * SPEED; 
    }
    public override void _Process(double delta) {
        Position += movementVector * (float)delta;
    }
    public void PlaySelfDestructAnimation() { animationPlayer.Play("Collide"); }
}