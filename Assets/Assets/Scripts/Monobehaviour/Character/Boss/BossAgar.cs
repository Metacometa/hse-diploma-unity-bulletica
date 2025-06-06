using UnityEngine;

public class BossAgar : Boss
{
    //public BossProfile profile;
    //SkillSet
    public BossAgarController controller;
    public int bouncesCount;

    /*[HideInInspector]*/ public int agarCounter;

    protected override void Awake()
    {
        base.Awake();

        controller = transform.parent.transform.GetComponent<BossAgarController>();

        bouncesCount = 0;
        agarCounter = 0;
    }

    private void Start()
    {

    }

    void OnEnable()
    {
        sleep.onSleep = false;

        ChooseStartDir();
    }

    protected override void Update() 
    {
        if (health.health == 0)
        {
            DeathHandler();
        }
    }

    protected override void FixedUpdate() 
    {
        rb.linearVelocity = rb.linearVelocity.normalized * profile.moveSpeed;
    }

    void DeathHandler()
    {
        if (agarCounter < controller.maxAgar)
        {
            controller.SpawnMiniAgars(transform, agarCounter, rb);
        }

        death.Die(gameObject);
    }

    void ChooseStartDir()
    {
        rb.linearVelocity = Random.insideUnitCircle * profile.moveSpeed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Obstacle"))
        {
            if (bouncesCount >= controller.bouncesToChase)
            {
                Vector2 toTarget = (target.position() - transform.position).normalized;
                rb.linearVelocity = toTarget.normalized * profile.moveSpeed;

                bouncesCount = 0;   
            }
            else
            {
                bouncesCount++;
            }
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            health.TakeDamage();

            if (health.health == 0)
            {
                rb.linearVelocity = collision.GetComponent<Rigidbody2D>().linearVelocity;
            }

            shimmer.ShimmerManager();
        }
    }
}
