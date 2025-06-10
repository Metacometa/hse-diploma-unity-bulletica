using UnityEngine;

[RequireComponent(typeof(BaseHealth))]
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(BaseDeath))]
[RequireComponent(typeof(BaseShimmer))]
[RequireComponent(typeof(BaseRotator))]
[RequireComponent(typeof(BaseSleep))]
[RequireComponent(typeof(BaseShooting))]

[RequireComponent(typeof(InputManager))]

[RequireComponent(typeof(PlayerDeath))]
[RequireComponent(typeof(PlayerHealth))]

public class Player : Gunman
{
    private InputManager input;
    private PlayerDeath playerDeath;
    private PlayerHealth playerHealth;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<InputManager>();

        playerDeath = GetComponent<PlayerDeath>();
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void SetStartPosition()
    {
        transform.position = new Vector3(0, 0, transform.position.z);
    }

    protected override void FixedUpdate()
    {
        /*        if (!health.isLostControl)
                {
                    move.Move(ref rb, input.moveDir);
                    shooting.RotateGun(input.aimDir);

                    if (!shooting.onAttack)
                    {
                        if (shooting.IsMagazineEmpty())
                        {
                            if (!shooting.onReload)
                            {
                                shooting.ReloadManager();
                            }

                        }
                        else
                        {
                            if (!shooting.onCooldown && !shooting.onReload)
                            {
                                //StartCoroutine(gun.ShootManager());
                            }
                        }
                    }

                    if (input.onAttackButton)
                    {
                        shooting.ShootingManager(input.aimDir);
                    }
                }*/

        //Vector2 dir = targetPosition - (Vector2)transform.position;


        rotator.RotateGun(input.aimDir - (Vector2)transform.position);

        if (input.moveDir != Vector2.zero)
        {
            move.Move(input.moveDir);
            rotator.Rotate(input.moveDir);
        }
        else
        {
            move.Stop();
            rotator.StopRotate();
        }

/*        if (shooting.IsMagazineEmpty())
        {
            shooting.ReloadManager();
        }
        else
        {
            shooting.ShootingManager();
        }*/

/*        if (!shooting.OnCooldown())
        {
            if (shooting.IsMagazineEmpty())
            {
                if (!shooting.onReload)
                {
                    shooting.ReloadManager();
                }
            }
            else
            {
                if (!shooting.onCooldown && !shooting.onReload)
                {

                    //StartCoroutine(gun.ShootManager());
                }
            }
        }*/

    }

    protected override void Update() 
    {
        if (playerHealth.health == 0)
        {
            playerDeath.Die(gameObject);
        }

        input.UpdateInput();

        if (shooting.IsMagazineEmpty())
        {
            if (!shooting.OnReload())
            {
                shooting.ReloadManager();
            }
        }
        else if (input.onAttackButton && 
            !shooting.OnCooldown() && !shooting.OnAttack() && !shooting.OnReload())
        {
            shooting.ShootingManager();
        }


    }

    protected override void OnCollisionStay2D(Collision2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.transform.CompareTag("Enemy"))
        {
            CollisionDamage(collision.collider);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.CompareTag("Bullet"))
        {
            CollisionDamage(collision);
        }
    }

    protected override void CollisionDamage(Collider2D collision)
    {
        if (invincibility.invincible) { return; }

        playerHealth.TakeDamage();
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
