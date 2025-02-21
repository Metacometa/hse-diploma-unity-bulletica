using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ChamberDoorsHelper : MonoBehaviour
{
    public List<Vector2> roomIntersectionPoints;
    [SerializeField] public float intersectionRadius;

    [SerializeField] public LayerMask mask;

    public List<Transform> nearbyChambers;

    void Start()
    {
        for (int i = 0; i < roomIntersectionPoints.Count; i++) 
        {
            roomIntersectionPoints[i] += (Vector2)transform.position;
        }

        //roomIntersectionPoints = new List<Vector2>();
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
                if (wallsCounter == 2 || (doorCounter == 2 && wallsCounter == 1))
                {
                    walls[i].gameObject.SetActive(false);
                    doors[i].gameObject.SetActive(true);
                }
            }
        }
    }

    public void GetDoors(ref List<Transform> doors)
    {
        Transform doorsTransform = transform.Find("Doors");

        if (doorsTransform != null)
        {
            doors.Add(doorsTransform.Find("DoorControllerLeft"));
            doors.Add(doorsTransform.Find("DoorControllerRight"));
            doors.Add(doorsTransform.Find("DoorControllerUp"));
            doors.Add(doorsTransform.Find("DoorControllerDown"));
        }
    }

    public void GetWalls(ref List<Transform> walls)
    {
        Transform grid = transform.Find("Grid");

        if (grid != null)
        {
            walls.Add(grid.Find("WallLeft"));
            walls.Add(grid.Find("WallRight"));
            walls.Add(grid.Find("WallUp"));
            walls.Add(grid.Find("WallDown"));
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
