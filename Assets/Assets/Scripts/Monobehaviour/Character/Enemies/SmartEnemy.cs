using UnityEngine;
using EnemyState;


[RequireComponent(typeof(SmartMovement))]
public class SmartEnemy : Gunman
{
    //public EnemyProfile profile;

    //EnemyState
    public ActionState actionState;
    public MotionState motionState;
    public SmartMovement smartMove;

    protected override void Awake()
    {
        base.Awake();
        smartMove = GetComponent<SmartMovement>();
    }

    protected override void FixedUpdate()
    {
        if (sleep.onSleep)
        {
            return;
        }

        Vector2 dir = target.position() - transform.position;
        switch (motionState)
        {
            case MotionState.MoveToTarget:

                break;
            case MotionState.Stay:
                smartMove.StopAgent();
                break;
            case MotionState.Regroup:
                //move.Move(ref rb, dir, profile.moveSpeed);
                break;
            case MotionState.Pursue:
                smartMove.Pursue(target.position());
                break;
            case MotionState.Sleep:
                smartMove.StopAgent();
                break;
            default:
                smartMove.StopAgent();
                break;
        }
    }

    protected override void Update()
    {
        if (sleep.onSleep)
        {
            return;
        }

        Vector2 dir = (target.position() - transform.position).normalized;

        if (health.health == 0)
        {   
            death.Die(gameObject);
        }

        Observe();
        UpdateActionState();
        UpdateMovingState();


        if (target.inSight)
        {
            rotator.RotateGun(target.position() - transform.position);
        }
        else if (smartMove.CanMove())
        {
            rotator.RotateGun(smartMove.GetMoveDir());
        }
        rotator.Rotate(smartMove.GetMoveDir());


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
            case ActionState.Pursue:

                break;
            case ActionState.Sleep:
                break;
            default:
                break;
        }

    }
    public override void UpdateActionState()
    {
        if (sleep.onSleep)
        {
            actionState = ActionState.Sleep;
        }
        else if (smartMove.onPush)
        {
            actionState = ActionState.Stun;
        }
        else if (shooting.IsMagazineEmpty())
        {
            actionState = ActionState.Reload;
        }
        else if (target.inShootingRange)
        {
            actionState = ActionState.Shoot;
        }
        else if (target.inPursueRange && !smartMove.onPosition)
        {
            actionState = ActionState.Pursue;
        }
        else
        {
            actionState = ActionState.Idle;
        }
    }

    public override void UpdateMovingState()
    {
        motionState = MotionState.Pursue;

        if (sleep.onSleep)
        {
            motionState = MotionState.Sleep;
            return;
        }
        else if (smartMove.onPush || smartMove.onPosition)
        {
            motionState = MotionState.Stay;
            return;
        }
        else if (actionState == ActionState.Shoot || shooting.OnCooldown())
        {
            if (((ShootingProfile)profile).shootingOnMove && target.inPursueRange)
            {
                motionState = MotionState.Pursue;
            }
            else
            {
                motionState = MotionState.Stay;
            }
        }
        else if (target.inPursueRange && !smartMove.onPosition)
        {
            motionState = MotionState.Pursue;
        }
        else
        {
            //описать функции roaming
            motionState = MotionState.Stay;
        }
    }

}
