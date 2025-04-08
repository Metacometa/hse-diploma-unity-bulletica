using UnityEngine;

[RequireComponent(typeof(BaseShooting))]
[RequireComponent(typeof(BaseTargeting))]
public class Gunman : BaseCharacter
{
    protected BaseShooting shooting;

    protected BaseTargeting target;

    protected bool targetApproached;
    protected bool inShootingRange;

    protected override void Awake()
    {
        base.Awake();
        shooting = GetComponent<BaseShooting>();

        target = GetComponent<BaseTargeting>();
        if (target)
        {
            target.SetTarget();
        }
    }
    protected virtual void Update()
    {
        if (health.health == 0)
        {
            death.Die(gameObject);
        }

        if (move.onPush)
        {
            move.StopMovement(ref rb);
        }
    }
    protected virtual void FixedUpdate() {}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (GetComponent<BaseInvincibility>().invincible) { return; }

            health.TakeDamage();
            shimmer.ShimmerManager();

            Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
            float force = collision.GetComponent<BaseBullet>().force;

            move.PushAway(ref rb, dir * force);
        }
    }
}
