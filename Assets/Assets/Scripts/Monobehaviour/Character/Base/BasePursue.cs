using TMPro;
using UnityEngine;

public class BasePursue : MonoBehaviour
{
    private Vector2 lastSeenPos;

    [SerializeField] float distanceToReach;

    private bool canPursue;

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
