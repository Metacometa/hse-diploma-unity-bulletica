using UnityEngine;
using EnemyState;
using static UnityEngine.UI.Image;
using Unity.VisualScripting;


[RequireComponent(typeof(SmartMovement))]
public class SmartEnemy : Gunman, IObservable, IStatable
{
    //public EnemyProfile profile;

    //EnemyState
    public ActionState actionState;
    public MotionState motionState;
    public SmartMovement smartMove;

    [SerializeField] private LayerMask masks;
    [SerializeField] private LayerMask sightMask;

    public BasePursue pursue;

    protected override void Awake()
    {
        base.Awake();

        pursue = GetComponent<BasePursue>();
        smartMove = GetComponent<SmartMovement>();
    }

    void OnEnable()
    {
        if (rotator)
        {
            Vector2 startDir = (target.target.position - transform.position).normalized;
            rotator.RotateInstantly(startDir);
            rotator.RotateGunInstantly(startDir);
        }
    }

    protected override void FixedUpdate()
    {
        Vector2 dir = target.target.position - transform.position;
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
                smartMove.Pursue(target.target.position);
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
        Vector2 dir = (target.target.position - transform.position).normalized;

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
    public void UpdateActionState()
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

    public void UpdateMovingState()
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

    void ShootingHandler()
    {
        if (!shooting.OnCooldown() && !shooting.OnReload() && !shooting.OnAttack())
        {
            shooting.ShootingManager();
        }
    }

    void ReloadHandler()
    {
        if (!shooting.OnReload())
        {
            shooting.ReloadManager();
        }
    }

    public void Observe()
    {
        Vector3 origin = shooting.gunController.muzzle.position;
        Vector3 destination = shooting.gunController.bulletSpawn.position;
        //Vector2 dir = origin - destination;
        Vector2 sigtDir = target.target.position - transform.position;

        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

        WideProjectileCheck(origin, destination, ((ShootingProfile)profile).shootingRange, profile.shootingMask, ref target.inShootingRange);
    }

    public void LookToPoint(in Vector3 origin, in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.target.tag);

        }
        else
        {
            boolFlag = false;
        }
    }
    
    public void WideProjectileCheck(in Vector3 origin, in Vector3 destination, in float length, in LayerMask masks, ref bool boolFlag)
    {
        Vector3 dir = origin - destination;
        RaycastHit2D hit = Physics2D.CircleCast(origin, ((ShootingProfile)profile).bulletRadius, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.target.tag);
        }
        else
        {
            boolFlag = false;
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        if (!shooting) { return; }
        Vector3 origin = shooting.gunController.muzzle.position;

        Vector2 shootingDirection = Vector2.zero;
        Vector2 aimingDirection = Vector2.zero;
        if (target != null)
        {
            shootingDirection = (origin - shooting.gunController.bulletSpawn.position).normalized;
            aimingDirection = (target.target.position - origin).normalized;
        }

        Vector2 sigtDir = (target.target.position - transform.position).normalized;

        DrawProjectileCheck(shootingDirection, origin);
        return;
        //Gizmos.color = Color.yellow;
        //Gizmos.DrawRay(transform.position, aimingDirection * profile.sight);

        //Gizmos.color = Color.green;
        //Gizmos.DrawRay(transform.position, shootingDirection * ((ShootingProfile)profile).shootingRange);

        if (targetApproached)
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.green;
        }

        Gizmos.DrawRay(origin, shootingDirection * ((ShootingProfile)profile).approachedDistance);

        if (target.inSight)
        {
            Gizmos.color = Color.blue;
        }
        else
        {
            Gizmos.color = Color.magenta;
        }

        //Gizmos.DrawRay(transform.position, sigtDir * ((ShootingProfile)profile).sight);

        return;

        // --- Рисуем сам CircleCast ---

        DrawProjectileCheck(shootingDirection, origin);


        // 4. (Опционально) Рисуем центральную линию каста
        //Gizmos.color = Color.gray; // Сделаем ее серой для отличия
        //Gizmos.DrawLine(origin, endPoint);
    }

    void DrawProjectileCheck(in Vector3 shootingDirection, in Vector3 origin)
    {
        if (target.inShootingRange)
        {
            Gizmos.color = Color.green;    // Заблокировано союзником
        }
        else
        {
            Gizmos.color = Color.red; // В редакторе до запуска - синий
        }

        Vector3 direction = (Vector3)shootingDirection;
        Vector3 endPoint = origin + direction * ((ShootingProfile)profile).shootingRange;

        // 1. Рисуем начальный круг
        // Gizmos.DrawWireSphere(origin, circleRadius); // Sphere выглядит как круг в 2D
        DrawWireDisk(origin, ((ShootingProfile)profile).bulletRadius, Gizmos.color); // Более "плоский" вид

        // 2. Рисуем конечный круг (в точке столкновения или на макс. дистанции)
        // Gizmos.DrawWireSphere(endPoint, circleRadius);
        DrawWireDisk(endPoint, ((ShootingProfile)profile).bulletRadius, Gizmos.color);

        // 3. Рисуем линии, соединяющие края кругов
        // Вычисляем вектор, перпендикулярный направлению (в 2D)
        Vector3 perpendicular = (Vector3)(new Vector2(-direction.y, direction.x).normalized) * ((ShootingProfile)profile).bulletRadius;

        // Рисуем две линии по краям
        Gizmos.DrawLine(origin + perpendicular, endPoint + perpendicular);
        Gizmos.DrawLine(origin - perpendicular, endPoint - perpendicular);
    }

    void DrawWireDisk(Vector3 position, float radius, Color color, int segments = 20)
    {
        Color previousColor = Gizmos.color;
        Gizmos.color = color;
        Vector3 startPoint = position + Vector3.right * radius; // Начинаем справа
        Vector3 lastPoint = startPoint;

        float angleStep = 360f / segments;
        for (int i = 1; i <= segments; i++)
        {
            float angle = i * angleStep * Mathf.Deg2Rad;
            Vector3 nextPoint = position + new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0) * radius;
            Gizmos.DrawLine(lastPoint, nextPoint);
            lastPoint = nextPoint;
        }
        Gizmos.color = previousColor; // Возвращаем исходный цвет Gizmos
    }

}
