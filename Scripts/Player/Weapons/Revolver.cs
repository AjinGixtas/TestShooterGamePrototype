using Godot;
public partial class Revolver : Weapon
{
    public override void NormalShoot()
    {
        if (currentAmmo < 1) return;
        base.NormalShoot();
        c_instance = NORMAL_BULLET_SCENE.Instantiate<Bullet>();
        ANIMATION_PLAYER.Play("NormalShoot");
        PLAYER.BULLET_CONTAINER.AddChild(c_instance);
        c_instance.GlobalPosition = BULLET_SPAWN_POINT.GlobalPosition;
        c_rotation = BULLET_SPAWN_POINT.GlobalRotation + Mathf.DegToRad(CURRENT_SPREAD) * (GD.Randf() - .5f);
        if (ROTATE_NORMAL_BULLET) c_instance.GlobalRotation = c_rotation;
        c_instance.Intialize(new(Mathf.Cos(c_rotation), Mathf.Sin(c_rotation)));
    }
    public override void SpecialShoot()
    {
        if (currentAmmo < 1) return;
        base.SpecialShoot();
        --currentAmmo; CURRENT_SPREAD += SPREAD_PER_SHOT; SPREAD_RECOVERY_SPEED = 0;
        c_instance = SPECIAL_BULLET_SCENE.Instantiate<Bullet>();
        PLAYER.BULLET_CONTAINER.AddChild(c_instance);
        c_instance.GlobalPosition = BULLET_SPAWN_POINT.GlobalPosition;
        if (ROTATE_SPECIAL_BULLET) c_instance.GlobalRotation = BULLET_SPAWN_POINT.GlobalRotation;
        c_instance.Intialize(new(Mathf.Cos(BULLET_SPAWN_POINT.GlobalRotation), Mathf.Sin(BULLET_SPAWN_POINT.GlobalRotation)));
    }
}