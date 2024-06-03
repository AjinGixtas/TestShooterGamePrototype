using Godot;

public partial class WeaponManager : Node2D
{
    [Export] Sword sword; [Export] Weapon[] weapons;
    [Export] public Node2D BULLET_CONTAINER;
    [Export] BulletIndicator BULLET_INDICATOR;
    int currentActiveWeaponIndex = 0;
    float deltaF;
    public int CURRENT_ACTIVE_WEAPON_INDEX
    {
        get { return currentActiveWeaponIndex; }
        set
        {
            weapons[currentActiveWeaponIndex].Visible = false;
            currentActiveWeaponIndex = value;
            weapons[currentActiveWeaponIndex].Visible = true;
            BULLET_INDICATOR.FrameCoords = new(0, currentActiveWeaponIndex);
            BULLET_INDICATOR.ChangeBulletBarType(weapons[currentActiveWeaponIndex].MAX_AMMO, weapons[currentActiveWeaponIndex].currentAmmo);
        }
    }
    public override void _Ready()
    {
        BULLET_INDICATOR.ChangeBulletBarType(weapons[currentActiveWeaponIndex].MAX_AMMO, weapons[currentActiveWeaponIndex].currentAmmo);
    }
    public override void _Process(double delta)
    {
        deltaF = (float)delta;
        LookAt(GetGlobalMousePosition());
        if (GlobalPosition.X > GetGlobalMousePosition().X) Scale = new(1, -1);
        else Scale = new(1, 1);
        if (Input.IsActionJustPressed("ui_weaponOne")) { CURRENT_ACTIVE_WEAPON_INDEX = 0; }
        else if (Input.IsActionJustPressed("ui_weaponTwo")) { CURRENT_ACTIVE_WEAPON_INDEX = 1; }
        else if (Input.IsActionJustPressed("ui_weaponThree")) { CURRENT_ACTIVE_WEAPON_INDEX = 2; }

        if (Input.IsActionJustPressed("ui_swingSword")) sword.Attack();

        if (Input.IsActionJustReleased("ui_shoot"))
        {
            weapons[CURRENT_ACTIVE_WEAPON_INDEX].Charge(0);
            BULLET_INDICATOR.ChangeBulletBarType(weapons[currentActiveWeaponIndex].MAX_AMMO, weapons[currentActiveWeaponIndex].currentAmmo);
        }
        else if (Input.IsActionPressed("ui_shoot")) weapons[CURRENT_ACTIVE_WEAPON_INDEX].Charge(deltaF);
        if (Input.IsActionJustPressed("ui_reload"))
        {
            GD.Print("RELOAD!");
            weapons[CURRENT_ACTIVE_WEAPON_INDEX].Reload();
            BULLET_INDICATOR.ChangeBulletBarType(weapons[currentActiveWeaponIndex].MAX_AMMO, weapons[currentActiveWeaponIndex].currentAmmo);
        }
    }
    public void RecoverAmmoThroughSlash(int amountOfTargetHit)
    {
        weapons[currentActiveWeaponIndex].RecoverAmmoThroughSlash(amountOfTargetHit);
        BULLET_INDICATOR.ChangeBulletBarType(weapons[currentActiveWeaponIndex].MAX_AMMO, weapons[currentActiveWeaponIndex].currentAmmo);
    }
}