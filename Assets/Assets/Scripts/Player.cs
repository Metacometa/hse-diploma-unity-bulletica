using UnityEngine;
using UnityEngine.U2D;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private Animator anim;
    private SpriteRenderer sprite;

    private Vector2 input;

    [SerializeField] Vector2 move_speed;
 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprite = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        Flip();
        UpdateAnimationsVariables();
    }

    void UpdateAnimationsVariables()
    {
        if (rb.linearVelocityX != 0 || rb.linearVelocityY != 0)
        {
            anim.SetBool("moved", true);
        }
        else
        {
            anim.SetBool("moved", false);
        }

        anim.SetBool("front", false);
        anim.SetBool("back", false);
        anim.SetBool("side", false);

        if (rb.linearVelocityY > 0)
        {
            anim.SetBool("back", true);
        }
        else if (rb.linearVelocityY < 0)
        {
            anim.SetBool("front", true);
        }
        else
        {
            anim.SetBool("side", true);
        }

        anim.SetInteger("x", Mathf.Abs(Sign(rb.linearVelocityX)));
        anim.SetInteger("y", Sign(rb.linearVelocityY));
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
