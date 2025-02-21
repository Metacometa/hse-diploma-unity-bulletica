using UnityEngine;
using EnemyState;

public class Enemy : Gunman, IObservable, IEnemyStatable
{
    public EnemyProfile profile;

    //EnemyState
    public ActionState actionState;
    public MotionState motionState;

    private BaseTargeting target;
    public bool targetApproached;

    public bool inShootingRange;

    [SerializeField] private LayerMask masks;

    private bool onSleep;

    protected override void Start()
    {
        base.Start();
        onSleep = true;

        target = GetComponent<BaseTargeting>();
        target.SetTarget();
    }

    public void Wake()
    {
        onSleep = false;
    }

    protected override void FixedUpdate()
    {
        if (onSleep)
        {
            return;
        }

        switch (motionState)
        {
            case MotionState.MoveToTarget:
                Vector2 dir = target.target.position - transform.position;
                move.Move(ref rb, dir);
                break;
            case MotionState.Stay:
                move.StopMovement(ref rb);
                break;
            case MotionState.Regroup:
                move.StopMovement(ref rb);
                break;
            default:
                move.StopMovement(ref rb);
                break;
        }
    }

    protected override void Update()
    {
        if (onSleep)
        {
            Vector2 startDir = (target.target.position - transform.position).normalized;
            shooting.RotateGunInstantly(startDir);
            return;
        }

        if (health.healthPoints == 0)
        {
            death.Die(gameObject);
        }

        Observe();
        UpdateState();

        Vector2 dir = (target.target.position - transform.position).normalized;
        shooting.RotateGun(dir);

        switch (actionState)
        {
            case ActionState.Shoot:
                ShootingHandler();

                break;
            case ActionState.Reload:
                ReloadHandler();

                break;
            case ActionState.Idle:
                break;
            default:
                break;
        }
    }

    public void UpdateState()
    {
        if (move.onPush)
        {
            actionState = ActionState.Stun;
            motionState = MotionState.Stay;
            return;
        }

        if (!shooting.IsMagazineEmpty())
        {
            actionState = GetShootingBehaviourState();
            motionState = GetShootingMoveState();
        }
        else
        {
            actionState = ActionState.Reload;
            motionState = MotionState.Regroup;
        }

    }
    MotionState GetShootingMoveState()
    {
        if (!target.targetSeen || targetApproached)
        {
            return MotionState.Stay;
        }
        else if (inShootingRange)
        {
            if (profile.shootingOnMove)
            {
                return MotionState.MoveToTarget;
            }
            else
            {
                return MotionState.Stay;
            }
        }
        else
        {
            return MotionState.MoveToTarget;
        }
    }

    ActionState GetShootingBehaviourState()
    {
        if (inShootingRange)
        {
            return ActionState.Shoot;
        }
        else
        {
            return ActionState.Idle;
        }
    }

    void ShootingHandler()
    {
        if (!shooting.onCooldown && !shooting.onReload)
        {
            shooting.ShootingManager();
        }
    }

    void ReloadHandler()
    {
        if (!shooting.onReload)
        {
            shooting.ReloadManager();
        }
    }

    public void Observe()
    {
        Vector2 dir = target.target.position - transform.position;

        LookToPoint(dir, profile.sight, masks, ref target.targetSeen);
        LookToPoint(dir, profile.shootingRange, masks, ref inShootingRange);
        LookToPoint(dir, profile.approachedDistance, masks, ref targetApproached);
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
        Gizmos.DrawRay(transform.position, aimingDirection * profile.sight);

        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, shootingDirection * profile.shootingRange);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, shootingDirection * profile.approachedDistance);
    }
}
