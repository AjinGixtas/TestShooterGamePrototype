using Godot;

public partial class Sword : Node2D {
    [Export] float DAMAGE;
    [Export] AnimationPlayer ANIMATION_PLAYER;
    [Export] WeaponManager WEAPON_MANAGER; [Export] Player PLAYER;
    bool canAttack = true;
    int amountOfTargetHit = 0;
    public void Attack() {
        if(!canAttack) return; 
        PLAYER.slowDuration += 0.4f;
        canAttack = false; ANIMATION_PLAYER.Play("SwingSword"); 
    }
    public void OnAttackCooldownTimerTimeout() {
        canAttack = true;
    }
    public void OnAreaEntered(Area2D area) { amountOfTargetHit += 1; }
    public void RecoverAmmoThroughSlash() {
        WEAPON_MANAGER.RecoverAmmoThroughSlash(amountOfTargetHit);
        amountOfTargetHit = 0;
    }
}