using DozerBullState;
using UnityEngine;

[RequireComponent(typeof(DozerBullBreakthrough))]
public class DozerBull : Boss
{
    public ActionState actionState;
    public MotionState motionState;

    public DozerBullMovement dozerMove;
    private DozerBullBreakthrough breakthrough;

    [SerializeField] public DozerBullProfile dozerProfile;

    protected override void Awake()
    {
        base.Awake();
        dozerMove = GetComponent<DozerBullMovement>();
        breakthrough = GetComponent<DozerBullBreakthrough>();

        //awakeEvents.AddListener(breakthrough.Cooldown);
    }

    protected override void FixedUpdate()
    {
        if (sleep.onSleep) { return; }

        Vector2 dir = target.position() - transform.position;

        switch (motionState)
        {
            case MotionState.Stay:
                dozerMove.Stop();
                break;
            case MotionState.Follow:
                dozerMove.Move(dir);
                rotator.Rotate(rb.linearVelocity.normalized);
                rotator.RotateGun(rb.linearVelocity.normalized);
                break;
            case MotionState.Breakthrough:
                breakthrough.Breakthrough();

                if (rb.linearVelocity != Vector2.zero)
                {
                    rotator.Rotate(rb.linearVelocity.normalized);
                    rotator.RotateGun(rb.linearVelocity.normalized);
                }

                break;
            case MotionState.Push:
                dozerMove.Push();
                break;
            default:
                dozerMove.Stop();
                break;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (health.health == 0)
        {
            death.Die(gameObject);
        }

        if (sleep.onSleep) { return; }

        Observe();
        UpdateMovingState();
    }

    protected void UpdateMovingState()
    {
        if (dozerMove.onPush)
        {
            motionState = MotionState.Push;
        }
        else if (breakthrough.onBreakthrough || (target.inShootingRange && breakthrough.IsAttackPossible()))
        {
            motionState = MotionState.Breakthrough;
        }
        else if (target.inPursueRange && dozerMove.CanMove())
        {
            motionState = MotionState.Follow;
        }
        else
        {
            motionState = MotionState.Stay;
        }
    }

    public void Observe()
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.position();
        //Vector2 dir = origin - destination;
        Vector2 dir = destination - origin;

        LookToPoint(transform.position, dir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, dir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

        WideProjectileCheck(origin, destination, ((ShootingProfile)profile).shootingRange, profile.shootingMask, ref target.inShootingRange);
/*        Debug.Log($"In shooting range: {target.inShootingRange}");
        Debug.Log($"In sight: {target.inSight}");
        Debug.Log($"In purse range: {target.inPursueRange}");*/
    }

    public void LookToPoint(in Vector3 origin, in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag)
    {
        RaycastHit2D hit = Physics2D.Raycast(origin, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.Tag());

            //Debug.DrawRay(origin, dir * length, Color.black);
        }
        else
        {
            boolFlag = false;
            //Debug.DrawLine(origin, origin + (Vector3)dir.normalized * length, Color.white);
            //Debug.DrawRay(origin, dir * length, Color.white);
        }
    }

    public void WideProjectileCheck(in Vector3 origin, in Vector3 destination, in float length, in LayerMask masks, ref bool boolFlag)
    {
        Vector3 dir = destination - origin;
        float radius = ((ShootingProfile)profile).bulletRadius;

        Vector3 shift = dir.normalized * profile.shootingRangeShift; 

        //radius = 1f;
        RaycastHit2D hit = Physics2D.CircleCast(origin + shift, radius, dir.normalized, length, masks);

        if (hit)
        {
            //Debug.DrawLine(origin, origin + dir.normalized * length, Color.green);
            boolFlag = hit.transform.CompareTag(target.Tag());
        }
        else
        {
            //Debug.DrawLine(origin, origin + dir.normalized * length, Color.red);
            boolFlag = false;
        }
    }

    public virtual void OnDrawGizmosSelected()
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.position();

        Vector3 dir = destination - origin;

        Vector3 shift = dir.normalized * profile.shootingRangeShift;
        //radius = 1f;

        DrawProjectileCheck(dir, origin + shift);
        return;
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
        Vector3 endPoint = origin + direction.normalized * ((ShootingProfile)profile).shootingRange;

        float radius = ((ShootingProfile)profile).bulletRadius;

        // 1. Рисуем начальный круг
        // Gizmos.DrawWireSphere(origin, circleRadius); // Sphere выглядит как круг в 2D
        DrawWireDisk(origin, radius, Gizmos.color); // Более "плоский" вид

        // 2. Рисуем конечный круг (в точке столкновения или на макс. дистанции)
        // Gizmos.DrawWireSphere(endPoint, circleRadius);
        DrawWireDisk(endPoint, radius, Gizmos.color);

        // 3. Рисуем линии, соединяющие края кругов
        // Вычисляем вектор, перпендикулярный направлению (в 2D)
        Vector3 perpendicular = (Vector3)(new Vector2(-direction.y, direction.x).normalized) * radius;

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