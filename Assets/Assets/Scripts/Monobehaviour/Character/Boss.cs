using UnityEngine;

[RequireComponent(typeof(BaseTargeting))]
public class Boss : BaseCharacter
{
    /*    //SkillSet
        public BossProfile profile;*/

    protected BaseTargeting target;
    protected bool targetApproached;

    protected override void Awake()
    {
        base.Awake();

        sleep.onSleep = false;

        target = GetComponent<BaseTargeting>();
        target.SetTarget();
    }

    protected virtual void Update()
    {
        if (health.health == 0)
        {
            death.Die(gameObject);
        }
    }

    protected virtual void FixedUpdate() { }

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (GetComponent<BaseInvincibility>().invincible) { return; }

            health.TakeDamage();
            shimmer.ShimmerManager();
        }
    }
}

