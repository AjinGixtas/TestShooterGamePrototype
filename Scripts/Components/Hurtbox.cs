using Godot;

public partial class Hurtbox : Area2D
{
	float currentHealth, currentShield, shieldDelayProgess, shieldRegenProgress;
	public float CURRENT_HEALTH { get { return currentHealth; } set { currentHealth = value; HEALTH_BAR.Value = value; } }
	public float CURRENT_SHIELD { 
		get { return currentShield; } 
		set {
			if(currentShield > value) { SHIELD_DELAY_PROGRESS = 0; SHIELD_REGEN_PROGRESS = 0; }
			if(value < 0) CURRENT_HEALTH += value;
			currentShield = value; 
		 	SHIELD_BAR.Value = value; 
			SHIELD_REGEN_BAR.Value = CURRENT_SHIELD + SHIELD_REGEN_PROGRESS;
		} 
	}
	float SHIELD_DELAY_PROGRESS { 
		get { return shieldDelayProgess; } 
		set { 
			shieldDelayProgess = value; 
			SHIELD_DELAY_BAR.Value = value; 
		} 
	}
	float SHIELD_REGEN_PROGRESS { 
		get { return shieldRegenProgress; } 
		set { 
			shieldRegenProgress = value; 
			SHIELD_REGEN_BAR.Value = CURRENT_SHIELD + SHIELD_REGEN_PROGRESS; 
		} 
	}
	float deltaF;
	[Export] float MAX_HEALTH, MAX_SHIELD;
	[Export] ProgressBar HEALTH_BAR, SHIELD_BAR, SHIELD_DELAY_BAR, SHIELD_REGEN_BAR;
	public override void _Ready()
	{
		CURRENT_HEALTH = MAX_HEALTH; 
		if(MAX_SHIELD != 0) {
			CURRENT_SHIELD = MAX_SHIELD;
			SHIELD_DELAY_PROGRESS = 3; SHIELD_REGEN_PROGRESS = 3;
		}
	}
	public override void _Process(double delta)
	{
		deltaF = (float)delta;
		if (MAX_SHIELD > 0)
		{
			SHIELD_DELAY_PROGRESS = Mathf.Min(3, SHIELD_DELAY_PROGRESS + deltaF);
			if (SHIELD_DELAY_PROGRESS == 3 && CURRENT_SHIELD < MAX_SHIELD)
			{
				SHIELD_REGEN_PROGRESS += deltaF;
				if (SHIELD_REGEN_PROGRESS >= 1) { SHIELD_REGEN_PROGRESS = 0; ++CURRENT_SHIELD; }
			}
		}
	}
	public void TakingDamage(float damage)
	{
		if (MAX_SHIELD == 0) { CURRENT_HEALTH -= damage; return; }
		CURRENT_SHIELD -= damage;
	}
}
