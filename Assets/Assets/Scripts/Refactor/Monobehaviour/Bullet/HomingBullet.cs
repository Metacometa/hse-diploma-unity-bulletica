using UnityEngine;

public class HomingBullet : BaseBullet, IHomingable
{
    protected Transform target;

    public virtual void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void Update()
    {
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    public virtual void FixedUpdate()
    {
        Homing(target.position);
    }

    public virtual void Homing(in Vector2 to)
    {
        Vector2 dir = to - (Vector2)transform.position;

        rb.linearVelocity = speed * dir.normalized;
    }
}
