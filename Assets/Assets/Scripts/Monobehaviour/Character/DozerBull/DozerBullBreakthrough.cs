using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.RuleTile.TilingRuleOutput;

public class DozerBullBreakthrough : MonoBehaviour
{
    private BaseTargeting target;

    private DozerBull main;
    private DozerBullProfile dozerProfile;

    private Rigidbody2D rb;

    private NavMeshAgent agent;

    public bool onCooldown;
    public bool onBreakthrough;

    public bool isOutOfRange;

    Vector3 dir = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        agent = GetComponent<NavMeshAgent>();


        target = GetComponent<BaseTargeting>();
        main = GetComponent<DozerBull>();

        if (main)
        {
            dozerProfile = main.dozerProfile;
        }


        onCooldown = false;
        onBreakthrough = false;
    }

    void FixedUpdate()
    {
        if (onBreakthrough)
        {
            rb.linearVelocity = dir.normalized * dozerProfile.breakthroughSpeed;
        }
    }

    public void Breakthrough()
    {
        if (!onCooldown && !onBreakthrough)
        {
            onBreakthrough = true;
            agent.enabled = false;
            dir = (target.position() - transform.position).normalized;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (onBreakthrough)
        {
            onBreakthrough = false;
            rb.linearVelocity = Vector3.zero;

            agent.enabled = true;
            StartCoroutine(BreakthroughCooldown());
        }
    }

    public bool IsAttackPossible()
    {
        return !IsPlayerOutOfRange() && !onCooldown;
    }

    private bool IsPlayerOutOfRange()
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.position();
        Vector3 dir = (destination - origin).normalized;

        bool outOfRange = false;

        main.LookToPoint(transform.position, dir, dozerProfile.attackRange, dozerProfile.breakthroughMask, ref outOfRange);

        return outOfRange;
    }

    private IEnumerator BreakthroughCooldown()
    {
        onCooldown = true;
        yield return new WaitForSeconds(dozerProfile.breakthroughCooldown);
        onCooldown = false;
    }
}
