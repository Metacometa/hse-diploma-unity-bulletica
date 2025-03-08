using System.Collections.Generic;
using UnityEngine;

public enum Directions{ Top, Right, Bottom, Left};

public class DoorsBuilder : MonoBehaviour
{
    public List<Vector2> roomIntersectionPoints;
    [SerializeField] public float intersectionRadius;

    [SerializeField] public LayerMask mask;

    public List<Transform> nearbyChambers;

    void Awake()
    {
        for (int i = 0; i < roomIntersectionPoints.Count; i++)
        {
            roomIntersectionPoints[i] =
                Quaternion.Euler(0, 0, transform.parent.localRotation.eulerAngles.z) * roomIntersectionPoints[i];
        }

        int rotationIndex = (int)transform.parent.localRotation.eulerAngles.z / 90;
        ShiftList(ref roomIntersectionPoints, rotationIndex);

        for (int i = 0; i < roomIntersectionPoints.Count; i++) 
        {
            roomIntersectionPoints[i] += (Vector2)transform.position;
        }
    }

    public void ShiftList<T>(ref List<T> transforms, in int repeats)
    {
        if (transforms.Count == 0) return;

        for (int repeat = 0; repeat < repeats; ++repeat)
        {
            for (int i = 1; i < transforms.Count; ++i)
            {
                (transforms[i - 1], transforms[i]) = (transforms[i], transforms[i - 1]);
            }
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
                    doorControllers.Add(hit.transform.GetComponentInParent<Door>().transform);
                }
            }
        }

        return doorControllers;
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
