using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Windows;

public class SmartMovement : BaseMovement
{
    private NavMeshAgent agent;

    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
    }
/*    protected virtual void Start()
    {
        onPush = false;
    }*/

    void Update()
    {
        timeCounter -= Time.deltaTime;
        timeCounter = Mathf.Clamp(timeCounter, 0f, profile.stayBufferTime);
    }

    public void SmartMove(in Vector2 point)
    {
        if (agent.isOnNavMesh)
        {
            agent.SetDestination(point);
        }
    }
}
