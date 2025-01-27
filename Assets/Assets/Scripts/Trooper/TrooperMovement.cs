using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class TrooperMovement : MonoBehaviour
{
    private TrooperManager manager;

    [SerializeField] public float pushAwayTime;

    private void Start()
    {
        manager = GetComponent<TrooperManager>();        
    }

    public void moveToTarget(ref Rigidbody2D rb, in Transform target)
    {
        Vector2 dir = (target.position - transform.position).normalized;

        rb.linearVelocity = dir * manager.profile.moveSpeed;
    }

    public void rotateToTarget(ref Rigidbody2D rb, in Transform target)
    {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void StopMovement(ref Rigidbody2D rb)
    {
        rb.linearVelocity = Vector2.zero;
    }
}