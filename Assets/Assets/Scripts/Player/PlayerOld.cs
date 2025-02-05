using UnityEngine;

public class PlayerOld : MonoBehaviour
{
    private PlayerHealth health;
    private PlayerInputs input;
    private GunManager gun;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    [SerializeField] Vector2 move_speed;

    [SerializeField] private int startingHealths;
    [HideInInspector] public int healths;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        health = GetComponent<PlayerHealth>();
        input = GetComponent<PlayerInputs>();
        gun = GetComponent<GunManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
            if (!health.isInvincible)
            {
                Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
                float force = collision.GetComponent<BulletManager>().force;

                health.TakeDamage();
                PushFromBullet(dir * force);
            }

            //Destroy(collision.transform.gameObject);
        }
    }

    void Update()
    {
        Flip();
    }

    private void FixedUpdate()
    {
/*        if (!health.isLostControl)
        {
            Move();
            gun.rotateToTarget(input.aiming);


            if (!input.attacking)
            {
                if (gun.isMagazineEmpty())
                {
                    if (!gun.onReload)
                    {
                        StartCoroutine(gun.Reload());
                    }

                }
                else
                {
                    if (!gun.onCooldown && !gun.onReload)
                    {
                        //StartCoroutine(gun.ShootManager());
                    }
                }
            }
            if (input.attacking)
            {
                gun.Shoot(input.aiming);
            }
        }*/
    }

    private void PushFromBullet(in Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir, ForceMode2D.Impulse);
    }

    private int Sign(float val)
    {
        if (val > 0)
        {
            return 1;
        }
        else if (val < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    void Move()
    {
        //rb.linearVelocity = new Vector2(input.movement.x, input.movement.y).normalized * move_speed;    
    }

    public void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }
    
    void Flip()
    {
        if (rb.linearVelocityX > 0 || rb.linearVelocityY != 0)
        {
            sprite.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            sprite.flipX = true;
        }
    }
}
