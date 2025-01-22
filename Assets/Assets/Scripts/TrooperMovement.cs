using UnityEngine;

public class TrooperMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;

    public void moveToTarget(ref Rigidbody2D rb, in Transform target)
    {
        Vector2 direction = (target.position - transform.position).normalized;

        rb.linearVelocity = direction * moveSpeed; 
    }

    public void StopMovement(ref Rigidbody2D rb)
    {
        rb.linearVelocity = Vector2.zero;
    }
}