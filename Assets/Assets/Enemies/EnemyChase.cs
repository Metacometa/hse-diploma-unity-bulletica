using UnityEngine;

public class EnemyChase : MonoBehaviour
{
    private EnemyData data;

    [SerializeField] public float sight;

    [HideInInspector] public Vector2 targetPosition;

    private void Start()
    {
        data = GetComponent<EnemyData>();
    }

    public void Observe(ref bool targetSeen)
    {
        Collider2D targetHit = Physics2D.OverlapCircle((Vector2)transform.position, sight, data.targetMask);

        if (targetHit)
        {
            targetPosition = (Vector2)targetHit.transform.position;
            targetSeen = true;
        }
        else
        {
            targetPosition = transform.position;
            targetSeen = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, sight);
    }
    /*    void MoveToTarget()
        {
            Vector2 direction = target_position - (Vector2)transform.position;

            if (direction.magnitude <= attack_radius)
            {
                enemy.rb.linearVelocity = Vector2.zero;
                enemy.anim.SetTrigger("attack");
            }
            else
            {
                rb.linearVelocity = direction.normalized * move_speed;
            }


        }*/
}
