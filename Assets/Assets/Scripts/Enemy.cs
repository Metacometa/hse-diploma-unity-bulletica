using UnityEngine;

public class Enemy : MonoBehaviour
{
    private SpriteRenderer sprite;
    private Rigidbody2D rb;

    [SerializeField] private Vector2 speed;

    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
    }
    private void FixedUpdate()
    {
        Flip();
        //SetAnimation();        
    }

    void Update()
    {

    }

    void Flip()
    {
        if (rb.linearVelocityX > 0)
        {
            sprite.flipX = false;
        }
        else
        {
            sprite.flipX = true;
        }
    }
}
