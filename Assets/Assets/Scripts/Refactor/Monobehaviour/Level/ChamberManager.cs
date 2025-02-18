using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class ChamberManager : MonoBehaviour
{
    private List<Vector2> roomIntersectionPoints;
    [SerializeField] private float intersectionRadius;

    [SerializeField] private LayerMask mask;

    private void Start()
    {
        roomIntersectionPoints = new List<Vector2>();
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

            Debug.Log(i + " := " + wallsCounter + " " + doorCounter);
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
        doors.Add(transform.Find("DoorControllerLeft"));
        doors.Add(transform.Find("DoorControllerRight"));
        doors.Add(transform.Find("DoorControllerUp"));
        doors.Add(transform.Find("DoorControllerDown"));
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

    public void GetPoints()
    {
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
        }
    }

    public void OnDrawGizmosSelected()
    {
        foreach (Vector2 pos in roomIntersectionPoints)
        {
            Gizmos.DrawWireSphere(pos, intersectionRadius);
        }
    }
}
