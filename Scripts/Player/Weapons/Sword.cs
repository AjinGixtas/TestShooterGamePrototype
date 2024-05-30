using Godot;

public partial class Sword : Node2D {
    [Export] float DAMAGE;
    [Export] AnimationPlayer ANIMATION_PLAYER;
    [Export] WeaponManager WEAPON_MANAGER; [Export] Player PLAYER;
    bool canAttack = true;
    public void Attack() {
        if(!canAttack) return; 
        PLAYER.slowDuration += 0.4f;
        canAttack = false; ANIMATION_PLAYER.Play("SwingSword"); 
    }
    public void OnAttackCooldownTimerTimeout() {
        canAttack = true;
    }
}