using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed;
    public float force;

    public bool enemy = true;

    public void ShapeBullet(in Vector2 dir, in float angle)
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.linearVelocity = Quaternion.AngleAxis(angle, Vector3.forward) * dir * speed;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (enemy)
        {
            if (col.transform.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
        else
        {
            if (!col.transform.CompareTag("Player"))
            {
                Destroy(gameObject);
            }
        }
    }
}
