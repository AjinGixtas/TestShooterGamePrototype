using System;
using Godot;
public partial class Weapon : Node2D
{
    [Export] protected float SPREAD_PER_SHOT, MAX_SPREAD_RECOVERY_SPEED, SPREAD_RECOVERY_SPEED, MIN_SPREAD, MAX_SPREAD, SPECIAL_CHARGE_THRESHOLD;
    [Export] public int MAX_AMMO; [Export] protected int AMMO_PER_SHIELD;
    [Export] protected bool ROTATE_NORMAL_BULLET, ROTATE_SPECIAL_BULLET;
    protected float CURRENT_SPREAD { 
        get { return currentSpread; }
        set { currentSpread = Mathf.Min(Mathf.Max(MIN_SPREAD, value), MAX_SPREAD); }
    }
    protected float currentSpread, currentSpreadRecoverySpeed, currentCharge;
    public int currentAmmo;
    protected Bullet c_instance;
    protected float c_rotation, deltaF;
    protected int c_requiredShield;
    [Export] protected PackedScene NORMAL_BULLET_SCENE, SPECIAL_BULLET_SCENE;
    [Export] protected Node2D BULLET_SPAWN_POINT;
    [Export] protected AnimationPlayer ANIMATION_PLAYER;

    [Export] protected Hurtbox PLAYER_HURTBOX;
    [Export] protected Player PLAYER;
    public override void _Ready()
    {
        currentAmmo = MAX_AMMO; currentSpread = MIN_SPREAD;
    }
    public override void _Process(double delta)
    {
        deltaF = (float)delta;
        currentSpreadRecoverySpeed = Mathf.Min(MAX_SPREAD_RECOVERY_SPEED, currentSpreadRecoverySpeed + SPREAD_RECOVERY_SPEED * deltaF);
        CURRENT_SPREAD = Mathf.Max(0, CURRENT_SPREAD - currentSpreadRecoverySpeed * deltaF);
    }
    public virtual void Charge(float delta)
    {
        if (delta > 0) { currentCharge += delta; return; }
        if (currentCharge >= SPECIAL_CHARGE_THRESHOLD) { SpecialShoot(); return; }
        NormalShoot();
    }
    public virtual void RecoverSpread(float delta)
    {
        currentSpreadRecoverySpeed = Mathf.Min(MAX_SPREAD_RECOVERY_SPEED, currentSpreadRecoverySpeed + SPREAD_RECOVERY_SPEED * delta);
        CURRENT_SPREAD = Mathf.Max(0, CURRENT_SPREAD - currentSpreadRecoverySpeed * delta);
    }
    public virtual void NormalShoot()
    {
        PLAYER.slowDuration += 0.4f; --currentAmmo;
        CURRENT_SPREAD += SPREAD_PER_SHOT; currentSpreadRecoverySpeed = 0;
        currentCharge = 0;
    }
    public virtual void SpecialShoot() { PLAYER.slowDuration += 0.4f; currentCharge = 0; }
    public virtual void Reload()
    {
        c_requiredShield = Mathf.CeilToInt((MAX_AMMO - currentAmmo) / (float)AMMO_PER_SHIELD);
        currentAmmo += Mathf.Min(Mathf.FloorToInt(PLAYER_HURTBOX.CURRENT_SHIELD), c_requiredShield) * AMMO_PER_SHIELD;
        PLAYER_HURTBOX.CURRENT_SHIELD = Mathf.Max(0, PLAYER_HURTBOX.CURRENT_SHIELD - c_requiredShield);
    }
}