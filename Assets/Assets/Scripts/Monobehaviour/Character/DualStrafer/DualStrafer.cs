using DualStraferState;
using UnityEngine;

[RequireComponent(typeof(DualStraferShooting))]
[RequireComponent(typeof(DualStraferSingleStrafe))]
[RequireComponent(typeof(DualStraferRageStrafe))]

public class DualStrafer : Boss
{
    //public BossDualProfile profile;

    public ActionState actionState;
    public MotionState motionState;

    [SerializeField] private LayerMask masks;

    public DualStraferShooting shooting;
    public DualStraferAbstractStrafe strafe;
    public DualStraferAbstractStrafe rageStrafe;

    protected bool inShootingRange;
    protected bool inStrafeRange;

    public DualStraferAbstractStrafe currentStrafe;
    private bool onRageStrafe;

    protected override void Awake()
    {
        base.Awake();

        shooting = GetComponent<DualStraferShooting>();
        strafe = GetComponent<DualStraferSingleStrafe>();
        rageStrafe = GetComponent<DualStraferRageStrafe>();

        currentStrafe = strafe;
        onRageStrafe = false;
    }

    void OnEnable()
    {
        if (shooting)
        {
            Vector2 startDir = (target.target.position - transform.position).normalized;
            rotator.RotateInstantly(startDir);
        }
    }

    protected override void Update()
    {
        Vector2 dir = (target.target.position - transform.position).normalized;

        if (health.health == 0)
        {
            death.Die(gameObject);
        }


        Observe();
        UpdateActionState();
        UpdateMovingState();

        if (health.health > ((DualStraferProfile)profile).endlessStrafeHealthTrigger)
        {
            currentStrafe = strafe;
        }
        else
        {
            currentStrafe = rageStrafe;
            onRageStrafe = true;
        }


        switch (actionState)
        {
            case ActionState.Strafe:
                if (!currentStrafe.OnStrafe)
                {
                    currentStrafe.StartStrafeHandler();
                    rotator.Rotate(dir);
                }
                AltertaneAttackHandler();

                break;
            case ActionState.Shoot:
                DoubleAttackHandler();
                rotator.Rotate(dir);
                break;
            case ActionState.Reload:
                ReloadHandler();
                rotator.Rotate(dir);
                break;
            case ActionState.Idle:
                break;
            case ActionState.Pursue:
                break;
            case ActionState.Sleep:
                break;
            default:
                break;
        }
    }

    protected override void FixedUpdate() {
        Vector2 dir = target.target.position - transform.position;

        switch (motionState)
        {
            case MotionState.Strafe:
                move.Move(currentStrafe.strafeDir, ((DualStraferProfile)profile).strafeSpeed);
                break;
            case MotionState.MoveToTarget:
                move.Move(dir);
                break;
            case MotionState.Stay:
                move.Stop();
                break;
            case MotionState.Regroup:
                move.Stop();
                break;
            case MotionState.Pursue:
/*                Vector2 pursueDir = pursue.lastSeenPos - (Vector2)transform.position;
                move.Move(ref rb, pursueDir);*/
                break;
            case MotionState.Sleep:
                move.Stop();
                break;
            default:
                move.Stop();
                break;
        }
    }

    public void UpdateActionState()
    {
        if (sleep.onSleep)
        {
            actionState = ActionState.Sleep;
        }
        else if (onRageStrafe && !currentStrafe.OnCooldown)
        {
            actionState = ActionState.Strafe;
        }
        else if ((inStrafeRange && !currentStrafe.OnCooldown) || currentStrafe.OnStrafe)
        {
            actionState = ActionState.Strafe;
        }
        else if (shooting.IsMagazineEmpty())
        {
            actionState = ActionState.Reload;
        }
        else if (inShootingRange)
        {
            actionState = ActionState.Shoot;
        }
        else
        {
            actionState = ActionState.Idle;
        }
    }
    public void UpdateMovingState()
    {
        if (targetApproached)
        {
            move.Buffering();
        }

        if (sleep.onSleep)
        {
            motionState = MotionState.Sleep;
        }
        else if (actionState == ActionState.Strafe)
        {
            motionState = MotionState.Strafe;
        }
        else if (!move.CanMove())
        {
            motionState = MotionState.Stay;
        }
        else if (actionState == ActionState.Reload)
        {
            if (target.inSight && !targetApproached)
            {
                motionState = MotionState.Regroup;
            }
            else
            {
                motionState = MotionState.Stay;
            }
        }
        else if (actionState == ActionState.Shoot || shooting.OnCooldown())
        {
            if (targetApproached)
            {
                motionState = MotionState.Stay;
            }
            else
            {
                motionState = MotionState.MoveToTarget;
            }
        }
        else if (target.inSight)
        {
            motionState = MotionState.MoveToTarget;
        }
        else
        {
            motionState = MotionState.Stay;
        }

    }

    public void Observe()
    {
        Vector2 dir = target.target.position - transform.position;

        LookToPoint(dir, profile.sightRange, masks, ref target.inSight);
        LookToPoint(dir, ((DualStraferProfile)profile).shootingRange, masks, ref inShootingRange);
        LookToPoint(dir, ((DualStraferProfile)profile).approachedDistance, masks, ref targetApproached);

        LookToPoint(dir, ((DualStraferProfile)profile).strafeActivationRadius, masks, ref inStrafeRange);
    }

    public void LookToPoint(in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.target.tag);
        }
        else
        {
            boolFlag = false;
        }
    }

    void DoubleAttackHandler()
    {
        if (!shooting.OnCooldown() && !shooting.OnReload() && !shooting.OnAttack() && !shooting.IsMagazineEmpty())
        {
            //shooting.ShootingManager();
            shooting.DoubleAttack();
        }
    }

    void AltertaneAttackHandler()
    {
        if (!shooting.OnCooldown() && !shooting.OnReload() && !shooting.OnAttack() && !shooting.IsMagazineEmpty())
        {
            //shooting.ShootingManager();
            shooting.AltertaneAttack();
        }
    }

    void ReloadHandler()
    {
        if (!shooting.OnReload())
        {
            shooting.ReloadManager();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("collision");
        currentStrafe.EndStrafeHandler();
    }

    public void OnDrawGizmosSelected()
    {
        Vector2 shootingDirection = Vector2.zero;
        Vector2 aimingDirection = Vector2.zero;
        if (target != null)
        {
            shootingDirection = (target.target.position - transform.position).normalized;
            aimingDirection = (target.target.position - transform.position).normalized;
        }

        Gizmos.color = Color.yellow;
        Gizmos.DrawRay(transform.position, aimingDirection * profile.sightRange);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, shootingDirection * ((ShootingProfile)profile).shootingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shootingDirection * ((ShootingProfile)profile).approachedDistance);

        Gizmos.color = Color.blue;
        Gizmos.DrawRay(transform.position, shootingDirection * ((DualStraferProfile)profile).strafeActivationRadius);
    }
}

