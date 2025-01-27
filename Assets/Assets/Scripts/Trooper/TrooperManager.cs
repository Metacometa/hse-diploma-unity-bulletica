using System.Collections;
using UnityEngine;

public class TrooperManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private TrooperData data;
    private TrooperCombat combat;
    private TrooperMovement move;
    private GunManager gun;

    [SerializeField] public EnemyProfile profile;

    [SerializeField] private LayerMask masks;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        data = GetComponent<TrooperData>();    
        combat = GetComponent<TrooperCombat>();
        move = GetComponent<TrooperMovement>();
        gun = GetComponent<GunManager>();
    }

    void Update()
    {
        Observe();
    }

    void FixedUpdate()
    {
        if (!data.attacking)
        {
            if (gun.isMagazineEmpty())
            {
                if (!gun.onReload)
                {
                    StartCoroutine(gun.Reload());
                }

            }
            else if (data.targetInShootingRange)
            {
                if (!gun.onCooldown && !gun.onReload)
                {
                    StartCoroutine(combat.ShootManager());
                }
            }
        }

        if (data.targetSeen)
        {
            move.rotateToTarget(ref rb, data.target);
        }

        if (data.canMove)
        {
            if ((profile.shootingOnTheMove || !data.attacking)
                && data.targetSeen
                && Vector2.Distance(transform.position, data.target.position) > profile.minDistance)
            {
                move.moveToTarget(ref rb, data.target);
            }
            else
            {
                move.StopMovement(ref rb);
            }
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Bullet")
        {
    /*        if (!health.isInvincible)
            {
                Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
                float force = collision.GetComponent<BulletManager>().force;

                health.TakeDamage();
                PushFromBullet(dir * force);
            }*/

            Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
            float force = collision.GetComponent<BulletManager>().force;
            PushFromBullet(dir * force);

            Debug.Log("Kek");
            //Destroy(collision.transform.gameObject);
        }
    }

    private void PushFromBullet(in Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir, ForceMode2D.Impulse);

        StartCoroutine(PushAway());
    }

    IEnumerator PushAway()
    {
        data.canMove = false;

        yield return new WaitForSeconds(move.pushAwayTime);

        data.canMove = true;
    }

    void Observe()
    {
        UpdateTargetInShootingRange();
        UpdateTargetSeen();
    }

    void UpdateTargetInShootingRange()
    {
        Vector2 shootingDirection = (data.target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootingDirection, profile.shootingRange, masks);

        if (hit)
        {
            data.targetInShootingRange = hit.transform.CompareTag("Player");
        }
        else
        {
            data.targetInShootingRange = false;
        }
    }

    void UpdateTargetSeen()
    {
        Vector2 aimingDirection = (data.target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimingDirection, profile.sight, masks);

        if (hit)
        {
            data.targetSeen = hit.transform.CompareTag("Player");
        }
        else
        {
            data.targetSeen = false;
        }
    }

    public void OnDrawGizmosSelected()
    {
        Vector2 shootingDirection = Vector2.zero;
        Vector2 aimingDirection = Vector2.zero;
        if (data != null)
        {
            shootingDirection = (data.target.position - transform.position).normalized;
            aimingDirection = (data.target.position - transform.position).normalized;
        }

        if (combat != null)
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawRay(transform.position, aimingDirection * profile.sight);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, shootingDirection * profile.shootingRange);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, shootingDirection * profile.minDistance);
        }
    }
}
