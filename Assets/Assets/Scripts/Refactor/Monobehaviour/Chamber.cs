using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    [SerializeField] private List<Vector2> roomIntersectionPoints;
    [SerializeField] private float intersectionRadius;
    [SerializeField] private LayerMask mask;

    public List<Transform> walls;
    public List<Transform> doors;

    public List<bool> on;

    void Start()
    {
        GetWalls();
        for (int i = 0; i < walls.Count; ++i)
        {
            on.Add(true);
        }

        GetDoors();

        CheckIntersections();
    }

    void CheckIntersections()
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

            if (wallsCounter == 2 || (doorCounter == 1 && wallsCounter == 1))
            {
                on[i] = false;
            }
        }
    }

    void Update()
    {
        ToogleWalls();
    }

    void ToogleWalls()
    {
        if (!(on.Count == walls.Count && on.Count == doors.Count))
        {
            return;
        }

        for (int i = 0; i < on.Count; ++i)
        {
            if (walls[i] != null)
            {
                walls[i].gameObject.SetActive(on[i]);
            }

            if (doors[i] != null)
            {
                doors[i].gameObject.SetActive(!on[i]);
            }
        }
    }

    void GetWalls()
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

    void GetDoors()
    {
        doors.Add(transform.Find("DoorControllerLeft"));
        doors.Add(transform.Find("DoorControllerRight"));
        doors.Add(transform.Find("DoorControllerUp"));
        doors.Add(transform.Find("DoorControllerDown"));
    }

    public void OnDrawGizmosSelected()
    {
        foreach (Vector2 pos in roomIntersectionPoints)
        {
            Gizmos.DrawWireSphere(pos, intersectionRadius);
        }
    }
}
