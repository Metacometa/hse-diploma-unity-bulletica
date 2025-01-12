using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private EnemyData data;
    private EnemyCombat combat;

    [SerializeField] private Vector2 moveSpeed;

    private void Start()
    {
        data = GetComponent<EnemyData>();
        combat = GetComponent<EnemyCombat>();       
    }

    public void Move(ref Rigidbody2D rb)
    {
        rb.linearVelocity = moveSpeed;
    }

    public void Chase(ref Rigidbody2D rb, in Vector2 target)
    {
        Vector2 direction = target - (Vector2)transform.position;
        //rb.linearVelocity = Vector2.down * moveSpeed;
        if (Mathf.Abs(direction.x) <= combat.attackRange)
        {
            float directionY = target.y - transform.position.y;

            combat.EstimateAttack(ref data.targetInAttackRange);
            if (!data.targetInAttackRange)
            {
                rb.linearVelocityY = directionY / (Mathf.Abs(directionY)) * moveSpeed.y;
                rb.linearVelocityX = 0;
                Debug.Log(rb.linearVelocity + " " + (Vector2)transform.position + " " + target);
            }
        }
        else
        {
            rb.linearVelocity = direction.normalized * moveSpeed;
        }
    }

    public void Stay(ref Rigidbody2D rb)
    {
        rb.linearVelocity = Vector2.zero;
    }
}
