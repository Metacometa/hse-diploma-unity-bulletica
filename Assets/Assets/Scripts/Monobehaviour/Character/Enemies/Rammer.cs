using UnityEngine;
using EnemyState;

[RequireComponent(typeof(SmartMovement))]
public class Rammer : Gunman
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

    void OnEnable()
    {
        if (rotator)
        {
        }
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


        rotator.RotateGun(smartMove.GetMoveDir());
        rotator.Rotate(smartMove.GetMoveDir());


        switch (actionState)
        {
            case ActionState.Shoot:
                ShootingHandler();

                move.Buffering();
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


    public override void Observe()
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.position();

        //Vector2 dir = origin - destination;
        Vector2 sigtDir = target.position() - transform.position;

        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

        WideProjectileCheck(origin, destination, ((ShootingProfile)profile).shootingRange, profile.shootingMask, ref target.inShootingRange);
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.transform.CompareTag("Player"))
        {
            death.Die(gameObject);
        }
    }

}
