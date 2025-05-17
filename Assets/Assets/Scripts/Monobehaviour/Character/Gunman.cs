using UnityEngine;

[RequireComponent(typeof(BaseShooting))]
[RequireComponent(typeof(BaseTargeting))]
public class Gunman : BaseCharacter
{
    protected BaseShooting shooting;

    protected BaseTargeting target;

    public bool targetApproached;
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
            move.Stop();
        }
    }
    protected virtual void FixedUpdate() {}

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.tag == "Bullet")
        {
            CollisionDamage(collision);
            invincibility.Invincible();
        }
    }
    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.transform.CompareTag("Player"))
        {
            CollisionDamage(collision.collider);
            invincibility.Invincible();
        }
    }

    protected void CollisionDamage(Collider2D collision)
    {
        if (invincibility.invincible) { return; }

        health.TakeDamage();
        shimmer.ShimmerManager();
        invincibility.Invincible();

        Rigidbody2D tempRb = collision.GetComponent<Rigidbody2D>();
        BaseBullet tempBullet = collision.GetComponent<BaseBullet>();

        if (tempRb && tempBullet)
        {
            Vector2 dir = tempRb.linearVelocity;
            float force = tempBullet.force;

            move.PushAway(ref rb, dir * force);
        }
    }
}
