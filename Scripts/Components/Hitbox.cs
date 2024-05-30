using Godot;

public partial class Hitbox : Area2D {
    [Export] float damage;
	public void OnAreaEntered(Area2D area) {
		if(!area.IsInGroup("Hurtbox")) return;
        (area as Hurtbox).TakingDamage(damage);
	}
}
