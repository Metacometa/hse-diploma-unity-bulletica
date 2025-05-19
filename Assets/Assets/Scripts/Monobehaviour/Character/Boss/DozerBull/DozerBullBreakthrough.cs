using System.Collections;
using UnityEngine;

public class DozerBullBreakthrough : MonoBehaviour
{
    private TurretShooting[] guns;
    private BaseTargeting target;

    private DozerBull main;
    private DozerBullProfile dozerProfile;

    private DozerBullMovement move;

    private Rigidbody2D rb;

    public bool onCooldown;
    public bool onBreakthrough;

    public bool isOutOfRange;

    Vector3 dir = Vector3.zero;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        target = GetComponent<BaseTargeting>();
        move = GetComponent<DozerBullMovement>();
        main = GetComponent<DozerBull>();

        if (main)
        {
            dozerProfile = main.dozerProfile;
        }

        onCooldown = false;
        onBreakthrough = false;

        guns = GetComponentsInChildren<TurretShooting>();
    }

    void FixedUpdate() {}

    public void Breakthrough()
    {
        if (!onCooldown && !onBreakthrough)
        {
            onBreakthrough = true;

            //dir = (target.position() - transform.position).normalized;
            dir = (target.PredictedPosition() - transform.position).normalized;

            rb.linearVelocity = dir.normalized * dozerProfile.breakthroughSpeed;
            DrainGuns();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (onBreakthrough)
        {
            onBreakthrough = false;
            rb.linearVelocity = Vector3.zero;
            move.PushAway();

            if (!onCooldown)
            {
                Cooldown();
            }
        }
    }

    void DrainGuns()
    {
        foreach(TurretShooting gun in guns)
        {
            gun.Drain();
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

    public void Cooldown()
    {
        Debug.Log("Cooldown");
        if (!onCooldown)
        {
            StartCoroutine(BreakthroughCooldown());
        }
    }

    private IEnumerator BreakthroughCooldown()
    {
        onCooldown = true;
        onBreakthrough = false;
        yield return new WaitForSeconds(dozerProfile.breakthroughCooldown);
        onCooldown = false;
    }
}
