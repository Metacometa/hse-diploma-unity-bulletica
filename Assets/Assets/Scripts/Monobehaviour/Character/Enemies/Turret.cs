using UnityEngine;
using EnemyState;

[RequireComponent(typeof(TurretShooting))]
public class Turret : Gunman
{
    public ActionState actionState;
    public MotionState motionState;

    public TurretShooting turretShooting;

    [SerializeField] public TurretProfile turretProfile;

    protected override void Awake()
    {
        base.Awake();
        turretShooting = GetComponent<TurretShooting>();

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

        UpdateAttack();
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

    private void UpdateAttack()
    {
        Vector3 origin = shooting.gunController.muzzle.position;
        Vector3 destination = shooting.gunController.bulletSpawn.position;
        //Vector2 dir = origin - destination;
        Vector2 dir = target.position() - transform.position;

        bool baseAttack = false;
        LookToPoint(origin, dir, turretProfile.attackChangeDistance, turretProfile.mask, ref baseAttack);

        if (baseAttack)
        {
            turretShooting.SetBaseAttack();
        }
        else
        {
            turretShooting.SetShotgunAttack();
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
        else if (turretShooting.IsMagazineEmpty())
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

    protected override void ShootingHandler()
    {
        if (!turretShooting.OnCooldown() && !turretShooting.OnReload() && !turretShooting.OnAttack())
        {
            turretShooting.ShootingManager();
        }
    }

    protected override void ReloadHandler()
    {
        if (!turretShooting.OnReload())
        {
            turretShooting.ReloadManager();
        }
    }
}
