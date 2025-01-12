using UnityEngine;
using UnityEngine.U2D;

public class EnemyManager : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private EnemyData data;
    private EnemyState state;
    private EnemyChase chase;
    private EnemyMovement move;
    private EnemyCombat combat;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        data = GetComponent<EnemyData>();
        state = GetComponent<EnemyState>();
        chase = GetComponent<EnemyChase>();
        move = GetComponent<EnemyMovement>();
        combat = GetComponent<EnemyCombat>();
    }

    private void Update()
    {
        combat.EstimateAttack(ref data.targetInAttackRange);
        chase.Observe(ref data.targetSeen);

        switch(state)
        {
            case move:
                {
                    break;
                }
            case idle:
                {
                    break;
                }
            case attack:
                {
                    break;
                }
        }

        if (data.targetInAttackRange)
        {
            move.Stay(ref rb);
            StartCoroutine(combat.Attack());

        }
        else if (data.targetSeen)
        {
            combat.EstimateAttack(ref data.targetInAttackRange);
            move.Chase(ref rb, chase.targetPosition);

            anim.Play("Walk");
        }
    }

    void FixedUpdate()
    {
        Flip();
        state.DefineState();
        /*        Flip();
                FindTarget();
                MoveToTarget();
                UpdateAnimationsVariables();
                SetAnimation();*/
    }

    public void Flip()
    {
        if (rb.linearVelocityX > 0)
        {
            transform.localScale = new Vector2(1, 1);
            //sprite.flipX = false;
        }
        else if (rb.linearVelocityX < 0)
        {
            transform.localScale = new Vector2(-1, 1);
            //sprite.flipX = true;
        }
    }
}
