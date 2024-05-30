using System;
using Godot;

public partial class Shotgun : Weapon
{
	[Export] int PELLET_PER_SHOT;
	public override void NormalShoot()
	{
		if (currentAmmo < 1) return;
		base.NormalShoot();
		for (int i = 0; i < PELLET_PER_SHOT; ++i)
		{
			c_instance = NORMAL_BULLET_SCENE.Instantiate<Bullet>();
			ANIMATION_PLAYER.Play("NormalShoot");
			PLAYER.BULLET_CONTAINER.AddChild(c_instance);
			c_instance.GlobalPosition = BULLET_SPAWN_POINT.GlobalPosition;
			c_rotation = BULLET_SPAWN_POINT.GlobalRotation + Mathf.DegToRad(CURRENT_SPREAD) * (GD.Randf() - .5f);
			if (ROTATE_NORMAL_BULLET) c_instance.GlobalRotation = c_rotation;
			c_instance.Intialize(new(Mathf.Cos(c_rotation), Mathf.Sin(c_rotation)));
		}
	}
	public override void SpecialShoot()
	{
		if (currentAmmo < 2) return;
		base.SpecialShoot();
		for (int i = 0; i < PELLET_PER_SHOT; ++i)
		{
			c_instance = SPECIAL_BULLET_SCENE.Instantiate<Bullet>();
			ANIMATION_PLAYER.Play("NormalShoot");
			PLAYER.BULLET_CONTAINER.AddChild(c_instance);
			c_instance.GlobalPosition = BULLET_SPAWN_POINT.GlobalPosition;
			c_rotation = BULLET_SPAWN_POINT.GlobalRotation + Mathf.DegToRad(CURRENT_SPREAD) * (GD.Randf() - .5f);
			if (ROTATE_SPECIAL_BULLET) c_instance.GlobalRotation = c_rotation;
			c_instance.Intialize(new(Mathf.Cos(c_rotation), Mathf.Sin(c_rotation)));
		}
		currentAmmo = 0;
	}
}
