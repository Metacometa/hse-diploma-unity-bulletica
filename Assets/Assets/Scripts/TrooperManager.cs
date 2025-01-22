using UnityEngine;

public class TrooperManager : MonoBehaviour
{
    private Rigidbody2D rb;

    private TrooperData data;
    private TrooperCombat combat;
    private TrooperMovement move;

    [SerializeField] private LayerMask masks;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        data = GetComponent<TrooperData>();    
        combat = GetComponent<TrooperCombat>();
        move = GetComponent<TrooperMovement>();
    }

    void Update()
    {
        Observe();

    }

    void FixedUpdate()
    {
        if (data.attacking || (!data.onReload && data.targetInShootingRange))
        {
            move.StopMovement(ref rb);
            if (!data.attacking)
            {
                StartCoroutine(combat.ShootManager());
            }
        }
        else if (data.targetSeen && Vector2.Distance(transform.position, data.target.position) > combat.minDistance)
        {
            move.moveToTarget(ref rb, data.target);
        }
        else
        {
            move.StopMovement(ref rb);
        }
    }

    void Observe()
    {
        UpdateTargetInShootingRange();
        UpdateTargetSeen();
    }

    void UpdateTargetInShootingRange()
    {
        Vector2 shootingDirection = (data.target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, shootingDirection, combat.shootingRange, masks);

        data.targetInShootingRange = !(bool)hit;
    }

    void UpdateTargetSeen()
    {
        Vector2 aimingDirection = (data.target.position - transform.position).normalized;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, aimingDirection, combat.sight, masks);

        data.targetSeen = hit ? false : true;
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
            Gizmos.DrawRay(transform.position, aimingDirection * combat.sight);

            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, shootingDirection * combat.shootingRange);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, shootingDirection * combat.minDistance);
        }
    }
}
