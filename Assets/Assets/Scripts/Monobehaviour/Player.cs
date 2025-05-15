using UnityEngine;

[RequireComponent(typeof(BaseHealth))]
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(BaseDeath))]
[RequireComponent(typeof(BaseShimmer))]
[RequireComponent(typeof(BaseSpawnArea))]
[RequireComponent(typeof(BaseRotator))]
[RequireComponent(typeof(BaseSleep))]
[RequireComponent(typeof(BaseShooting))]

[RequireComponent(typeof(InputManager))]

[RequireComponent(typeof(PlayerDeath))]

public class Player : Gunman
{
    private InputManager input;
    private PlayerDeath playerDeath;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<InputManager>();

        playerDeath = GetComponent<PlayerDeath>();
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

        move.Move(input.moveDir);
        rotator.RotateGun(input.aimDir - (Vector2)transform.position);

        if (input.moveDir != Vector2.zero)
        {
            rotator.Rotate(input.moveDir);
        }

        /*        if (!shooting.onAttack)
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
        if (health.health == 0)
        {
            playerDeath.Die(gameObject);
        }

        input.UpdateInput();

        if (input.onAttackButton && !shooting.OnCooldown() && !shooting.OnAttack())
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
            invincibility.Invincible();
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.CompareTag("Bullet"))
        {
            CollisionDamage(collision);
            invincibility.Invincible();
        }
    }
}
