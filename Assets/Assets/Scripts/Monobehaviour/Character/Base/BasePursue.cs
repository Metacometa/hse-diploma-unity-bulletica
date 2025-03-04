using TMPro;
using UnityEngine;

public class BasePursue : MonoBehaviour
{
    public Vector2 lastSeenPos;

    [SerializeField] float distanceToReach;

    public bool canPursue;

    private void Awake()
    {
        canPursue = false;
    }

    private void Update()
    {

    }

    public void UpdateCanPursue()
    {
        if (Vector2.Distance(lastSeenPos, transform.position) <= distanceToReach)
        {
            canPursue = false;
        }
    }

    public void UpdateLastSeenPos(in Vector2 pos)
    {
        lastSeenPos = pos;


        canPursue = true;
    }
}
