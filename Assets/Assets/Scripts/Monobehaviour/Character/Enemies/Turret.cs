using UnityEngine;
using EnemyState;

public class Turret : Gunman
{
    public ActionState actionState;
    public MotionState motionState;

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


        rotator.RotateGun(dir);
        rotator.Rotate(dir);


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

    protected override void FixedUpdate()
    { }

    public override void UpdateActionState()
    {
        if (sleep.onSleep)
        {
            actionState = ActionState.Sleep;
        }
        else if (shooting.IsMagazineEmpty())
        {
            actionState = ActionState.Reload;
        }
        else if (target.inShootingRange)
        {
            actionState = ActionState.Shoot;
        }
        else
        {
            actionState = ActionState.Idle;
        }
    }

    public override void UpdateMovingState()
    {

    }
}
