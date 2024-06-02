using Godot;

public partial class Player : CharacterBody2D {
    // Immutable during run-time
    [Export] float NORMAL_SPEED, DASH_SPEED_MULT, SLOW_SPEED_MULT;
    float DASH_SPEED, SLOW_SPEED, SLOW_DASH_SPEED;
    [Export] Timer DASH_DURATION_TIMER, DASH_COOLDOWN_TIMER;
    [Export] public Node2D BULLET_CONTAINER;
    [Export] Hurtbox HURTBOX;
    const float deltaF = 1f / 60f;
    // Cache
    Vector2 c_direction;
    // Run-time variable
    bool canDash = true, isDashing;
    public float slowDuration; float invincibleDuration;
    public float INVINCIBLE_DURATION { get{ return invincibleDuration; } set { invincibleDuration = value; HURTBOX.canTakeDamage = invincibleDuration <= 0; } }
    public override void _Ready()
    {
        DASH_SPEED = NORMAL_SPEED * DASH_SPEED_MULT; SLOW_SPEED = NORMAL_SPEED * SLOW_SPEED_MULT;
        SLOW_DASH_SPEED = NORMAL_SPEED * SLOW_SPEED_MULT * DASH_SPEED_MULT;
        Enemy.PLAYER = this;
    }
    public override void _Process(double delta) {
        c_direction = Input.GetVector("ui_moveLeft", "ui_moveRight", "ui_moveUp", "ui_moveDown").Normalized();
        if(Input.IsActionJustPressed("ui_dash")) Dash();
        slowDuration = Mathf.Max(slowDuration - deltaF, 0);
        INVINCIBLE_DURATION = Mathf.Max(INVINCIBLE_DURATION - deltaF, 0);
        if(!isDashing && slowDuration <= 0) Velocity = c_direction * NORMAL_SPEED;
        else if(isDashing && slowDuration > 0) Velocity = c_direction * SLOW_DASH_SPEED;
        else if(isDashing) Velocity = c_direction * DASH_SPEED;
        else if(slowDuration > 0) Velocity = c_direction * SLOW_SPEED;
        MoveAndSlide();
    }
    void Dash() {
        if(!canDash) return;
        canDash = false; isDashing = true; 
        DASH_DURATION_TIMER.Start(); DASH_COOLDOWN_TIMER.Start();
    }
    public void OnDashDurationTimerTimeout() { isDashing = false; }

    public void OnDashCooldownTimerTimeout() { canDash = true; }
    public void OnTakingDamage() { INVINCIBLE_DURATION += .8f; }
}