using Godot;

public partial class Sword : Node2D {
    [Export] float NORMAL_SLASH_DAMAGE, SPECIAL_SLASH_DAMAGE, SLASH_CHARGE_THRESHOLD;
    [Export] AnimationPlayer ANIMATION_PLAYER, PLAYER_ANIMATION_PLAYER;
    [Export] WeaponManager WEAPON_MANAGER; [Export] Player PLAYER;
    bool canAttack = true;
    float currentSlashCharge = 0;
    int amountOfTargetHit = 0;
    public void Charge(float delta) { 
        if(delta > 0) { currentSlashCharge += delta; PLAYER_ANIMATION_PLAYER.Play("ChargeSwordSwing"); return; }
        if(currentSlashCharge >= SLASH_CHARGE_THRESHOLD) { SpecialAttack(); return; }
        Attack();
    }
    public void SpecialAttack() {
        PLAYER_ANIMATION_PLAYER.Play("SwingNormalSword");
        ANIMATION_PLAYER.Play("SwingNormalSword"); 
    }
    public void Attack() {
        if(!canAttack) return; 
        PLAYER.slowDuration += 0.4f;
        canAttack = false; 
        PLAYER_ANIMATION_PLAYER.Play("SwingNormalSword");
        ANIMATION_PLAYER.Play("SwingNormalSword"); 
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