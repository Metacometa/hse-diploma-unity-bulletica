using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Chamber : MonoBehaviour
{
    private List<Vector2> roomIntersectionPoints;

    [SerializeField] private float intersectionRadius;
    [SerializeField] private LayerMask mask;

    public List<Transform> walls;
    public List<Transform> doors;

    public List<bool> doorOn;

    void Start()
    {
        GetWalls();
        for (int i = 0; i < walls.Count; ++i)
        {
            doorOn.Add(true);
        }

        GetDoors();

        Transform grid = transform.Find("Grid");
        if (grid != null)
        {
            Tilemap wallsTilemap = grid.Find("Walls").GetComponent<Tilemap>();

            if (wallsTilemap != null)
            {
                roomIntersectionPoints.Add((Vector2)wallsTilemap.transform.position + new Vector2(wallsTilemap.localBounds.min.x, 0));
                roomIntersectionPoints.Add((Vector2)wallsTilemap.transform.position + new Vector2(wallsTilemap.localBounds.max.x, 0));

                roomIntersectionPoints.Add((Vector2)wallsTilemap.transform.position + new Vector2(0, wallsTilemap.localBounds.max.y));
                roomIntersectionPoints.Add((Vector2)wallsTilemap.transform.position + new Vector2(0, wallsTilemap.localBounds.min.y));
            }

/*            roomIntersectionPoints1.Add(new Vector2(wallsTilemap.size.x, 0));
            wallsTilemap.localBounds*/
        }
    }

    public void OpenAllDoors()
    {
        foreach(Transform door in doors)
        {
            door.GetComponent<Door>().opening = true;
        }
    }

    public void CloseAllDoors()
    {
        foreach (Transform door in doors)
        {
            door.GetComponent<Door>().opening = false;
        }
    }

    public void CheckIntersections()
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

            if (i < doorOn.Count)
            {
                if (wallsCounter == 2 || (doorCounter == 1 && wallsCounter == 1))
                {
                    doorOn[i] = true;
                }
                else
                {
                    doorOn[i] = false;
                }
            }

        }
    }

    void Update()
    {
        ToogleWalls();
    }

    void ToogleWalls()
    {
        if (!(doorOn.Count == walls.Count && doorOn.Count == doors.Count))
        {
            return;
        }

        for (int i = 0; i < doorOn.Count; ++i)
        {
            if (walls[i] != null)
            {
                walls[i].gameObject.SetActive(!doorOn[i]);
            }

            if (doors[i] != null)
            {
                doors[i].gameObject.SetActive(doorOn[i]);
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
