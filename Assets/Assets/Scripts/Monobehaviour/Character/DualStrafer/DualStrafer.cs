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

    public DualStraferAbstractStrafe currentStrafe;

    protected override void Awake()
    {
        base.Awake();

        shooting = GetComponent<DualStraferShooting>();
        strafe = GetComponent<DualStraferSingleStrafe>();
        rageStrafe = GetComponent<DualStraferRageStrafe>();

        currentStrafe = strafe;
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

        if (profile.health > ((DualStraferProfile)profile).endlessStrafeHealthTrigger)
        {
            currentStrafe = strafe;
        }
        else
        {
            currentStrafe = rageStrafe;
        }

        switch (actionState)
        {
            case ActionState.Strafe:
                if (!currentStrafe.OnStrafe)
                {
                    currentStrafe.StartStrafeHandler();
                    rotator.Rotate(dir, shooting.GetRotationSpeed());
                }
                AltertaneAttackHandler();

                break;
            case ActionState.Shoot:
                DoubleAttackHandler();
                rotator.Rotate(dir, shooting.GetRotationSpeed());
                break;
            case ActionState.Reload:
                ReloadHandler();
                rotator.Rotate(dir, 300);
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
                move.Move(ref rb, currentStrafe.strafeDir, ((DualStraferProfile)profile).strafeSpeed);
                break;
            case MotionState.MoveToTarget:
                move.Move(ref rb, dir, profile.moveSpeed);
                break;
            case MotionState.Stay:
                move.StopMovement(ref rb);
                break;
            case MotionState.Regroup:
                move.StopMovement(ref rb);
                break;
            case MotionState.Pursue:
/*                Vector2 pursueDir = pursue.lastSeenPos - (Vector2)transform.position;
                move.Move(ref rb, pursueDir);*/
                break;
            case MotionState.Sleep:
                move.StopMovement(ref rb);
                break;
            default:
                move.StopMovement(ref rb);
                break;
        }
    }

    public void UpdateActionState()
    {
        if (sleep.onSleep)
        {
            actionState = ActionState.Sleep;
        }
        else if (!currentStrafe.OnCooldown)
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
        if (sleep.onSleep)
        {
            motionState = MotionState.Sleep;
        }
        else if (!currentStrafe.OnCooldown)
        {
            motionState = MotionState.Strafe;
        }
        else if (actionState == ActionState.Reload)
        {
            if (target.targetSeen && !targetApproached)
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
        else if (target.targetSeen)
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

        LookToPoint(dir, profile.sight, masks, ref target.targetSeen);
        LookToPoint(dir, ((DualStraferProfile)profile).shootingRange, masks, ref inShootingRange);
        LookToPoint(dir, ((DualStraferProfile)profile).approachedDistance, masks, ref targetApproached);
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
}

