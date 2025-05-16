using DozerBullState;
using UnityEngine;

[RequireComponent(typeof(DozerBullMovement))]
public class DozerBull : Boss
{
    public ActionState actionState;
    public MotionState motionState;

    public SmartMovement smartMove;

    // Use this for initialization
    void Start()
    {

    }

    protected override void FixedUpdate()
    {
        if (sleep.onSleep) { return; }

        Vector2 dir = target.target.position - transform.position;

        switch (motionState)
        {
            case MotionState.Stay:
                smartMove.Stop();
                break;
            case MotionState.Follow:
                smartMove.Move(dir);
                break;
            case MotionState.Breakthrough:
                move.Stop();
                break;
            default:
                move.Stop();
                break;
        }
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (sleep.onSleep) { return; }

        UpdateMovingState();
    }
    public void UpdateMovingState()
    {
        if (targetApproached)
        {
            move.Buffering();
        }

/*        if (!move.CanMove())
        {
            motionState = MotionState.Stay;
        }*/

        //Stay будет только в случае стана

        if (target.inShootingRange)
        {
            motionState = MotionState.Follow;
        }
        else
        {

        }

    }

    public void Observe()
    {
        Vector3 origin = transform.position;
        Vector3 destination = target.target.position;
        //Vector2 dir = origin - destination;
        Vector2 dir = target.target.position - transform.position;

        LookToPoint(transform.position, dir, ((ShootingProfile)profile).sightRange, profile.sightMask, ref target.inSight);
        LookToPoint(transform.position, dir, ((ShootingProfile)profile).pursueRange, profile.pursueMask, ref target.inPursueRange);

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
        float radius = ((ShootingProfile)profile).bulletRadius + 0.1f;
        RaycastHit2D hit = Physics2D.CircleCast(origin + dir.normalized * radius, radius, dir.normalized, length, masks);

        if (hit)
        {
            boolFlag = hit.transform.CompareTag(target.target.tag);
        }
        else
        {
            boolFlag = false;
        }
    }

}