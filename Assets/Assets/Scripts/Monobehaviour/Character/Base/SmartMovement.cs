
using System.Collections;
using Unity.Jobs;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public class SmartMovement : BaseMovement
{
    private NavMeshAgent agent;
    private NavMeshObstacle obstacle;
    public float moveUpdateTimer = 0f;
    public float moveUpdateCooldown = 1f;

    public LayerMask changePositionMask;
    private BaseTargeting target;

    //public float obstacleDisablerTimer = 0f;
    //public float obstacleDisablerCooldown = 1f;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;

        target = GetComponent<BaseTargeting>();

        NavMesh.avoidancePredictionTime = 0.5f;
        NavMesh.pathfindingIterationsPerFrame = 1000;

        obstacle = GetComponent<NavMeshObstacle>();
        obstacle.enabled = false;
    }
/*    protected virtual void Start()
    {
        onPush = false;
    }*/

    void Update()
    {
        timeCounter -= Time.deltaTime;
        timeCounter = Mathf.Clamp(timeCounter, 0f, profile.stayBufferTime);


        moveUpdateTimer -= Time.deltaTime;
        //obstacleDisablerTimer -= Time.deltaTime;
    }

    private void SetDestination(in Vector2 point)
    {
        obstacle.enabled = false;
        /*        if (obstacleDisablerTimer <= 0f)
                {
                    obstacle.enabled = false;
                    //agent.enabled = true;
                    agent.isStopped = false;

                    obstacleDisablerTimer = obstacleDisablerCooldown;
                }*/
        if (moveUpdateTimer <= 0f)
        {
            //agent.SetDestination(newPoint);
            StartCoroutine(MoveAgent(point));
            moveUpdateTimer = moveUpdateCooldown;
        }
    }

    private IEnumerator MoveAgent(Vector3 point)
    {
        yield return null;
        agent.enabled = true;

        if (agent.isOnNavMesh)
        {
            Vector3 newPoint = RandomPoint(point);
            agent.SetDestination(point);
        }

    }

    public void Pursue(in Vector2 point)
    {
        Vector2 dir = target.target.position - transform.position;
        float length = ((ShootingProfile)profile).changePositionRadius;

        bool changePosition = true;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, changePositionMask);

        if (hit) 
        { 
            if (hit.transform.CompareTag(target.target.tag))
            {
                Debug.DrawRay(transform.position, dir, Color.green);
                changePosition = false;
            }
            else
            {
                Debug.DrawRay(transform.position, dir, Color.yellow);
            }
        }
        else
        {
            Debug.DrawRay(transform.position, dir, Color.red);
        }
        //Debug.Log(hit.transform.tag + " " + changePosition);

        if (changePosition)
        {
            Debug.Log("changePosition");
            SetDestination(point);
        }
        else
        {
            Debug.Log("stop");
            StopAgent();
        }

        return;

        if (agent.isOnNavMesh
            && moveUpdateTimer <= 0f 
            && changePosition)
        {
            agent.SetDestination(RandomPoint(target.target.position));
            moveUpdateTimer = moveUpdateCooldown;
        }

        if (agent.isOnNavMesh
            && moveUpdateTimer <= 0f
            && !changePosition)
        {
            //SmartStop();
        }
    }

    public Vector3 RandomPoint(in Vector2 point)
    {
        NavMeshPath path = new NavMeshPath();
        Vector3 destination = new Vector3(1000,1000,1000);
        bool correctPath = false;

        int iterations = 0;

        while (iterations <= 10000 && !correctPath)
        {
            NavMeshHit hit;

            float radius = ((ShootingProfile)profile).maxPositionRadius;
            float shiftPosition = ((ShootingProfile)profile).shiftPositionCloser;
            Vector2 shift = ((Vector2)transform.position - point).normalized * shiftPosition;

            Vector2 generatedPoint = shift + point + Random.insideUnitCircle * radius;

            NavMesh.SamplePosition(generatedPoint, out hit, radius, NavMesh.AllAreas);

            destination = hit.position;

            if (((ShootingProfile)profile).minPositionRadius <= Vector2.Distance(point, destination)
                && Vector2.Distance(point, destination) <= ((ShootingProfile)profile).maxPositionRadius)
            {
                if (destination.y > -100000 & destination.y < 100000)
                {
                    agent.CalculatePath(destination, path);

                    correctPath = path.status == NavMeshPathStatus.PathComplete
                        && !NavMesh.Raycast(target.target.position, destination, out hit, NavMesh.AllAreas);
                }
            }
            iterations++;
        }
        //Debug.Log("Distance: " + Vector2.Distance(point, destination) + "\n Iteration: " + iterations);

        return destination;
    }

    public bool OnPosition()
    {
        Vector2 dir = target.target.position - transform.position;
        float length = ((ShootingProfile)profile).changePositionRadius;

        bool onPosition = false;
        RaycastHit2D hit = Physics2D.Raycast(transform.position, dir, length, changePositionMask);
        if (hit)
        {
            if (hit.transform.CompareTag(target.target.tag))
            {
                onPosition = true;
            }
        }

        return onPosition;
    }

    public void StopAgent()
    {
        if (moveUpdateTimer <= 0f)
        {
            agent.enabled = false;
            obstacle.enabled = true;
        }

        return;
        if (agent.isOnNavMesh
            && moveUpdateTimer <= 0f)
        {


            moveUpdateTimer = moveUpdateCooldown;
        }
    }

    public Vector3 GetMoveDir()
    {
        return agent.desiredVelocity.normalized;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;

        //Gizmos.DrawWireSphere(transform.position, 3f);

        Gizmos.DrawLine(transform.position, transform.position + agent.desiredVelocity); // Куда хочет агент
        
        Gizmos.color = Color.green;
        //Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + (Vector3)rb.linearVelocity); // Куда реально движется RB
                                                                                    // 
        //Gizmos.DrawWireSphere(agent.desiredVelocity, 3f);
    }
}
