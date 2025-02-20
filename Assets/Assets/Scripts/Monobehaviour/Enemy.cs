using UnityEngine;

public class Enemy : Gunman, IObservable
{
    public EnemyProfile profile;

    private BaseTargeting target;

    public bool canShoot;

    [SerializeField] private LayerMask masks;

    protected override void Start()
    {
        base.Start();

        target = GetComponent<BaseTargeting>();
        target.SetTarget();

        Vector2 dir = (target.target.position - transform.position).normalized;
        shooting.RotateGunInstantly(dir);
    }

    protected override void FixedUpdate()
    {
        if (move.canMove)
        {
            if ((profile.shootingOnTheMove || !shooting.onAttack)
                && target.targetSeen
                && Vector2.Distance(transform.position, target.target.position) > profile.minDistance)
            {
                Vector2 dir = target.target.position - transform.position;
                move.Move(ref rb, dir);
            }
            else
            {
                move.StopMovement(ref rb);
            }
        }

    }

    protected override void Update()
    {
        Observe();

        if (health.healthPoints == 0)
        {
            death.Die(gameObject);
        }

        if (!shooting.onAttack)
        {
            if (shooting.IsMagazineEmpty())
            {
                if (!shooting.onReload)
                {
                    shooting.ReloadManager();
                }
            }
            else if (canShoot)
            {
                if (!shooting.onCooldown && !shooting.onReload)
                {
                    shooting.ShootingManager();
                }
            }
        }

        if (target.targetSeen)
        {
            Vector2 dir = (target.target.position - transform.position).normalized;
            shooting.RotateGun(dir);
        }
    }

    public void Observe()
    {
        Vector2 dir = target.target.position - transform.position;

        LookToPoint(dir, profile.sight, masks, ref target.targetSeen);
        LookToPoint(dir, profile.shootingRange, masks, ref canShoot);
    }

    public void LookToPoint(in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.target.tag);
        }
        else
        {
            boolFlag = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Vector2 shootingDirection = Vector2.zero;
        Vector2 aimingDirection = Vector2.zero;
        if (target != null)
        {
            shootingDirection = (target.target.position - transform.position).normalized;
            aimingDirection = (target.target.position - transform.position).normalized;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, aimingDirection * profile.sight);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, shootingDirection * profile.shootingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shootingDirection * profile.minDistance);
    }
}
