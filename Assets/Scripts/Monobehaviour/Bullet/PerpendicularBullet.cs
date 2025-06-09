using UnityEngine;

public class PerpendicularBullet : HomingBullet
{
    private bool velocityChanged;
    [SerializeField] private float inaccuracy;

    public override void Start()
    {
        base.Start();
        velocityChanged = false;
    }

    public override void FixedUpdate()
    {
        Homing(target.position);
    }

    public override void Homing(in Vector2 to)
    {
        Vector2 dirToPlayer = to - (Vector2)transform.position;
        Vector2 dirRb = rb.linearVelocity;

        if (!velocityChanged && Mathf.Abs(Vector2.Dot(dirToPlayer.normalized, dirRb.normalized)) <= inaccuracy)
        {
            rb.linearVelocity = speed * dirToPlayer.normalized;

            velocityChanged = true;
        }
    }
}
