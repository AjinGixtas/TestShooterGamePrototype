using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class Rifle : Weapon
{
	[Export] RayCast2D RAY_CAST; [Export] int NORMAL_SHOT_TARGET, SPECIAL_SHOT_TARGET;
	[Export] int NORMAL_SHOT_DAMAGE, SPECIAL_SHOT_DAMAGE;
	[Export] NinePatchRect TRACER;
	public override void NormalShoot()
	{
		if (currentAmmo < 1) return;
		base.NormalShoot();
		ANIMATION_PLAYER.Play("NormalShoot");
		RAY_CAST.GlobalRotation = GlobalRotation + (GD.Randf() - .5f) * Mathf.DegToRad(currentSpread);
		for (int i = 0; i < NORMAL_SHOT_TARGET; ++i)
			if (RAY_CAST.IsColliding())
			{
				TRACER.Size = new((TRACER.GlobalPosition - RAY_CAST.GetCollisionPoint()).Length(), 3);
				(RAY_CAST.GetCollider() as Hurtbox).TakingDamage(NORMAL_SHOT_DAMAGE);
			}
	}
	readonly List<Hurtbox> collidedHurtboxs = new(3);
	public override void SpecialShoot()
	{
		if (currentAmmo < 2) return;
		base.SpecialShoot();
		ANIMATION_PLAYER.Play("NormalShoot");
		int i = 0;
		RAY_CAST.GlobalRotation = GlobalRotation;
		while (i < SPECIAL_SHOT_TARGET - 1 && RAY_CAST.IsColliding())
		{
			collidedHurtboxs.Add(RAY_CAST.GetCollider() as Hurtbox);
			TRACER.Size = new((RAY_CAST.Position - RAY_CAST.GetCollisionPoint()).Length(), 1);
			collidedHurtboxs.Last().TakingDamage(SPECIAL_SHOT_DAMAGE);
			RAY_CAST.AddException(collidedHurtboxs.Last());
			RAY_CAST.ForceRaycastUpdate();
			++i;
		}
		foreach (Hurtbox collidedHurtbox in collidedHurtboxs)
		{
			RAY_CAST.RemoveException(collidedHurtbox);
		}
	}
}
