using UnityEngine;
using EnemyState;

[RequireComponent(typeof(SmartMovement))]
public class Rammer : Gunman, IObservable, IStatable
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
        Vector3 origin = transform.position;
        Vector3 destination = target.position();

        //Vector2 dir = origin - destination;
        Vector2 sigtDir = target.position() - transform.position;

        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, sigtDir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

        WideProjectileCheck(origin, destination, ((ShootingProfile)profile).shootingRange, profile.shootingMask, ref target.inShootingRange);
    }

    public void LookToPoint(in Vector3 origin, in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.Tag());

        }
        else
        {
            boolFlag = false;
        }
    }
    
    public void WideProjectileCheck(in Vector3 origin, in Vector3 destination, in float length, in LayerMask masks, ref bool boolFlag)
    {
        Vector3 dir = origin - destination;
        float radius = ((ShootingProfile)profile).bulletRadius + 0.1f;
        RaycastHit2D hit = Physics2D.CircleCast(origin + dir.normalized * radius, radius, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.Tag());
        }
        else
        {
            boolFlag = false;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (invincibility.invincible) { return; }

        if (collision.transform.CompareTag("Player"))
        {
            death.Die(gameObject);
        }
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
