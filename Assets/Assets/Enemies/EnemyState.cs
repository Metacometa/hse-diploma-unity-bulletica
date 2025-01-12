using UnityEngine;

public enum Enemy {Idle, Move, Attack}

public class EnemyState : MonoBehaviour
{
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private Animator anim;

    private EnemyData data;
    private EnemyChase chase;
    private EnemyMovement move;
    private EnemyCombat combat;

    [HideInInspector] public Enemy state;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        data = GetComponent<EnemyData>();
        chase = GetComponent<EnemyChase>();
        move = GetComponent<EnemyMovement>();
        combat = GetComponent<EnemyCombat>();
    }

    public void DefineState()
    {
        if (data.attacking)
        {
            state = Enemy.Attack;
        }
        else if (data.targetSeen)
        {
            combat.EstimateAttack(ref data.targetInAttackRange);
            move.Chase(ref rb, chase.targetPosition);

            anim.Play("Walk");

            state = Enemy.Move;
        }
        else
        {
            move.Stay(ref rb);

            anim.Play("Idle");

            state = Enemy.Idle;
        }

    }
    
}
