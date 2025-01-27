using UnityEngine;

public class BulletManager : MonoBehaviour
{
    private Rigidbody2D rb;
    public float force;

    public void ShapeBullet(in Vector2 dir, in float angle, in float speed, in float bulletForce)
    {
        rb = GetComponent<Rigidbody2D>();  
        rb.linearVelocity = Quaternion.AngleAxis(angle, Vector3.forward) * dir * speed;

        force = bulletForce;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
        /*        if (!collision.CompareTag("Player"))
                {
                    Destroy(gameObject);
                }*/
    }
}
