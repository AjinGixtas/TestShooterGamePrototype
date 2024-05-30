using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Rifle : Weapon
{
	[Export] RayCast2D rayCast; [Export] int NORMAL_SHOT_TARGET, SPECIAL_SHOT_TARGET;
	[Export] int NORMAL_SHOT_DAMAGE, SPECIAL_SHOT_DAMAGE;
	public override void NormalShoot() {
		if (currentAmmo < 1) return;
		base.NormalShoot();
        ANIMATION_PLAYER.Play("NormalShoot");
		rayCast.Enabled = true;
		for (int i = 0; i < NORMAL_SHOT_TARGET; ++i) 
			if(rayCast.IsColliding()) { 
				(rayCast.GetCollider() as Hurtbox).TakingDamage(NORMAL_SHOT_DAMAGE);
			}
	}
    List<Hurtbox> collidedHurtboxs = new List<Hurtbox>(3);
	public override void SpecialShoot()
	{
		if (currentAmmo < 2) return;
		base.SpecialShoot();
        ANIMATION_PLAYER.Play("NormalShoot");
		rayCast.Enabled = true;
		int i = 0;
		while(i < SPECIAL_SHOT_TARGET && rayCast.IsColliding()) {
			collidedHurtboxs.Add(rayCast.GetCollider() as Hurtbox);
				collidedHurtboxs.Last().TakingDamage(SPECIAL_SHOT_DAMAGE);
			rayCast.AddException(collidedHurtboxs.Last());
			rayCast.ForceRaycastUpdate();
			++i;
		}
		foreach(Hurtbox collidedHurtbox in collidedHurtboxs) {
			rayCast.RemoveException(collidedHurtbox);
		}
	}
}
