using Godot;

public partial class BulletIndicator : Sprite2D
{
	[Export] ProgressBar BULLET_BAR;
	public void ChangeBulletBarType(int maxAmmo, int currentAmmo) {
		BULLET_BAR.MaxValue = maxAmmo; BULLET_BAR.Value = currentAmmo;
	}
}
