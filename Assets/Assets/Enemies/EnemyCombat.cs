using System.Collections;
using UnityEngine;

public class EnemyCombat : MonoBehaviour
{
    private Animator anim;

    private EnemyData data;

    [SerializeField] private float attackDuration;
    [SerializeField] public float attackRange;

    [SerializeField] private Vector2 attackHitPosition;
    [SerializeField] public Vector2 attackHitBox;

    private void Start()
    {
        anim = GetComponent<Animator>();

        data = GetComponent<EnemyData>();

        RuntimeAnimatorController ac = anim.runtimeAnimatorController;

        for (int i = 0; i < ac.animationClips.Length; i++)
            if (ac.animationClips[i].name == "Attack")
                attackDuration = ac.animationClips[i].length;
    }

    public IEnumerator Attack()
    {
        anim.Play("Attack");
        data.attacking = true;
        yield return new WaitForSeconds(attackDuration);
        data.attacking = false;
    }

    public void EstimateAttack(ref bool targetInAttackRange)
    {
        //Collider2D targetHit = Physics2D.OverlapCircle((Vector2)transform.position, attackRange, data.targetMask);
        Collider2D targetHit = Physics2D.OverlapBox(transform.position + transform.localScale.x * (Vector3)attackHitPosition,
            attackHitBox, 0, data.targetMask);

        if (targetHit)
        {
            float targetY = targetHit.transform.position.y;
            if (Mathf.Abs(targetY - transform.position.y) <= 0.1)
            {
                targetInAttackRange = true;
            }
            else
            {
                targetInAttackRange = false;
            }
        }
        else
        {
            targetInAttackRange = false;
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.DrawWireCube((transform.position + transform.localScale.x * (Vector3)attackHitPosition), attackHitBox);
    }

    /*    IEnumerator Fade()
        {
            for (float ft = 1f; ft >= 0; ft -= 0.1f)
            {
                Color c = renderer.material.color;
                c.a = ft;
                renderer.material.color = c;
                yield return new WaitForSeconds(.1f);
            }
        }*/
}
