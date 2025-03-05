using System.Collections.Generic;
using UnityEngine;

public enum Directions{ Top, Right, Bottom, Left};

public class ChamberDoorsHelper : MonoBehaviour
{
    public List<Vector2> roomIntersectionPoints;
    [SerializeField] public float intersectionRadius;

    [SerializeField] public LayerMask mask;

    public List<Transform> nearbyChambers;

    public int rotation;

    void Awake()
    {
        for (int i = 0; i < roomIntersectionPoints.Count; i++)
        {
            roomIntersectionPoints[i] =
                Quaternion.Euler(0, 0, rotation) * roomIntersectionPoints[i];
        }

        for (int i = 0; i < roomIntersectionPoints.Count; i++) 
        {
            roomIntersectionPoints[i] += (Vector2)transform.position;
        }
    }

    public HashSet<Transform> GetDoorControllers()
    {
        HashSet<Transform> doorControllers = new HashSet<Transform>();

        foreach (Vector2 v in roomIntersectionPoints)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(v, intersectionRadius, Vector2.zero, 0, mask);

            foreach(RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "Door")
                {
                    doorControllers.Add(hit.transform.parent);
                }
            }
        }

        return doorControllers;
    }

    public void GetNearbyChambers()
    {
        HashSet<Transform> neighbors = new HashSet<Transform>();

        foreach (Vector2 v in roomIntersectionPoints)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(v, intersectionRadius, Vector2.zero, 0, mask);

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "Door")
                {
                    neighbors.Add(hit.transform.parent.parent.parent);
                }
            }
        }

        foreach(Transform t in neighbors)
        { 
            if (t != null && t != transform)
            {
                nearbyChambers.Add(t);
            }

        }
    }

    public void WallsToDoors(ref List<Transform> walls, ref List<Transform> doors)
    {
        for (int i = 0; i < roomIntersectionPoints.Count; ++i)
        {
            RaycastHit2D[] hits = Physics2D.CircleCastAll(roomIntersectionPoints[i], intersectionRadius, Vector2.zero, 0, mask);

            int wallsCounter = 0;
            int doorCounter = 0;

            foreach (RaycastHit2D hit in hits)
            {
                if (hit.transform.tag == "WallFaker")
                {
                    wallsCounter++;
                }
                else if (hit.transform.tag == "Door")
                {
                    doorCounter++;
                }
            }

            if (i < walls.Count && i < doors.Count)
            {
                if (doorCounter != 4)
                {
                    walls[i].gameObject.SetActive(true);
                    doors[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void OnDrawGizmosSelected()
    {
        if (roomIntersectionPoints != null)
        {
            foreach (Vector2 pos in roomIntersectionPoints)
            {
                Gizmos.DrawWireSphere(pos, intersectionRadius);
            }
        }
    }
}
