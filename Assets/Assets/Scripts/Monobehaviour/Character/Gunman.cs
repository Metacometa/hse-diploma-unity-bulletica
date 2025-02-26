using UnityEngine;

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
    }
    protected virtual void Update()
    {
        if (health.healthPoints == 0)
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
            health.TakeDamage();
            shimmer.ShimmerManager();

            Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
            float force = collision.GetComponent<BaseBullet>().force;

            move.PushAway(ref rb, dir * force);
        }
    }
}
