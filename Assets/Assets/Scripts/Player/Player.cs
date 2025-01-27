using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    private PlayerHealth health;

    private Rigidbody2D rb;
    private SpriteRenderer sprite;

    private Vector2 input;

    [SerializeField] Vector2 move_speed;

    [SerializeField] private int startingHealths;
    [HideInInspector] public int healths;
    
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();

        health = GetComponent<PlayerHealth>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            if (!health.isInvincible)
            {
                Vector2 dir = collision.GetComponent<Rigidbody2D>().linearVelocity;
                float force = collision.GetComponent<BulletManager>().force;

                health.TakeDamage();
                PushFromBullet(dir * force);
            }

            Destroy(collision.transform.gameObject);
        }
    }

    void Update()
    {
        GetInput();
        Flip();
    }

    private void FixedUpdate()
    {
        if (!health.isLostControl)
        {
            Move();
        }
    }

    private void PushFromBullet(in Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir, ForceMode2D.Impulse);
    }

    private int Sign(float val)
    {
        if (val > 0)
        {
            return 1;
        }
        else if (val < 0)
        {
            return -1;
        }
        else
        {
            return 0;
        }
    }

    void GetInput()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
    }

    void Move()
    {
        rb.linearVelocity = new Vector2(input.x, input.y).normalized * move_speed;

        return;
        if (input.x > 0)
        {
            rb.linearVelocityX = move_speed.x;
        }
        else if (input.x < 0)
        {
            rb.linearVelocityX = -move_speed.x;
        }
        else
        {
            rb.linearVelocityX = 0;
        }

        if (Input.GetAxis("Vertical") > 0)
        {
            rb.linearVelocityY = move_speed.y;
        }
        else if (Input.GetAxis("Vertical") < 0)
        {
            rb.linearVelocityY = -move_speed.y;
        }
        else
        {
            rb.linearVelocityY = 0;
        }
    }

    public void StopMoving()
    {
        rb.linearVelocity = Vector2.zero;
    }
    
    void Flip()
    {
        if (rb.linearVelocityX > 0 || rb.linearVelocityY != 0)
        {
            sprite.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            sprite.flipX = true;
        }
    }
}
