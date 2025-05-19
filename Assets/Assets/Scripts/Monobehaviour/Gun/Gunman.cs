using UnityEngine;

[RequireComponent(typeof(BaseShooting))]
[RequireComponent(typeof(BaseTargeting))]
public class Gunman : BaseCharacter, IObservable, IStatable
{
    protected BaseShooting shooting;

    protected BaseTargeting target;

    public bool targetApproached;
    protected bool inShootingRange;

    void OnEnable()
    {
        if (rotator)
        {
        }
    }

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
            BaseBullet bullet = collision.GetComponent<BaseBullet>();
            if (!bullet.enemy)
            {
                CollisionDamage(collision);
                invincibility.Invincible();
            }
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

    public virtual void UpdateActionState()
    {
    }

    public virtual void UpdateMovingState()
    {
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

    protected virtual void ShootingHandler()
    {
        if (!shooting.OnCooldown() && !shooting.OnReload() && !shooting.OnAttack())
        {
            shooting.ShootingManager();
        }
    }

    protected virtual void ReloadHandler()
    {
        if (!shooting.OnReload())
        {
            shooting.ReloadManager();
        }
    }

    public virtual void Observe()
    {
        Vector3 origin = shooting.gunController.muzzle.position;
        Vector3 destination = shooting.gunController.bulletSpawn.position;
        //Vector2 dir = origin - destination;
        Vector2 sigtDir = target.position() - transform.position;

        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

        WideProjectileCheck(origin, destination, ((ShootingProfile)profile).shootingRange, profile.shootingMask, ref target.inShootingRange);
    }

    public virtual void LookToPoint(in Vector3 origin, in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.Tag());

        }
        else
        {
            boolFlag = false;
        }
    }

    public virtual void WideProjectileCheck(in Vector3 origin, in Vector3 destination, in float length, in LayerMask masks, ref bool boolFlag)
    {
        Vector3 dir = origin - destination;
        float radius = ((ShootingProfile)profile).bulletRadius + 0.1f;
        RaycastHit2D hit = Physics2D.CircleCast(origin + dir.normalized * radius, radius, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.Tag());
        }
        else
        {
            boolFlag = false;
        }
    }


    public virtual void OnDrawGizmosSelected()
    {
        if (!shooting) { return; }

        Vector3 origin = shooting.gunController.muzzle.position;


        Vector2 shootingDirection = Vector2.zero;
        Vector2 aimingDirection = Vector2.zero;
        if (target != null)
        {
            shootingDirection = (origin - shooting.gunController.bulletSpawn.position).normalized;
            aimingDirection = (target.position() - origin).normalized;
        }

        Vector2 sigtDir = (target.position() - transform.position).normalized;

        DrawProjectileCheck(shootingDirection, origin);
        return;
        /*        //Gizmos.color = Color.yellow;
                //Gizmos.DrawRay(transform.position, aimingDirection * profile.sight);

                //Gizmos.color = Color.green;
                //Gizmos.DrawRay(transform.position, shootingDirection * ((ShootingProfile)profile).shootingRange);

                if (targetApproached)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawRay(origin, shootingDirection * ((ShootingProfile)profile).approachedDistance);

                if (target.inSight)
                {
                    Gizmos.color = Color.blue;
                }
                else
                {
                    Gizmos.color = Color.magenta;
                }

                //Gizmos.DrawRay(transform.position, sigtDir * ((ShootingProfile)profile).sight);

                return;

                // --- Рисуем сам CircleCast ---

                DrawProjectileCheck(shootingDirection, origin);*/


        // 4. (Опционально) Рисуем центральную линию каста
        //Gizmos.color = Color.gray; // Сделаем ее серой для отличия
        //Gizmos.DrawLine(origin, endPoint);
    }

    public virtual void DrawProjectileCheck(in Vector3 shootingDirection, in Vector3 origin)
    {
        if (target.inShootingRange)
        {
            Gizmos.color = Color.green;    // Заблокировано союзником
        }
        else
        {
            Gizmos.color = Color.red; // В редакторе до запуска - синий
        }

        Vector3 direction = (Vector3)shootingDirection;
        Vector3 endPoint = origin + direction * ((ShootingProfile)profile).shootingRange;

        // 1. Рисуем начальный круг
        // Gizmos.DrawWireSphere(origin, circleRadius); // Sphere выглядит как круг в 2D
        DrawWireDisk(origin, ((ShootingProfile)profile).bulletRadius, Gizmos.color); // Более "плоский" вид

        // 2. Рисуем конечный круг (в точке столкновения или на макс. дистанции)
        // Gizmos.DrawWireSphere(endPoint, circleRadius);
        DrawWireDisk(endPoint, ((ShootingProfile)profile).bulletRadius, Gizmos.color);

        // 3. Рисуем линии, соединяющие края кругов
        // Вычисляем вектор, перпендикулярный направлению (в 2D)
        Vector3 perpendicular = (Vector3)(new Vector2(-direction.y, direction.x).normalized) * ((ShootingProfile)profile).bulletRadius;

        // Рисуем две линии по краям
        Gizmos.DrawLine(origin + perpendicular, endPoint + perpendicular);
        Gizmos.DrawLine(origin - perpendicular, endPoint - perpendicular);
    }

    public virtual void DrawWireDisk(Vector3 position, float radius, Color color, int segments = 20)
    {
        Color previousColor = Gizmos.color;
        Gizmos.color = color;
        Vector3 startPoint = position + Vector3.right * radius; // Начинаем справа
        Vector3 lastPoint = startPoint;

        float angleStep = 360f / segments;
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.color = previousColor; // Возвращаем исходный цвет Gizmos
    }

}
