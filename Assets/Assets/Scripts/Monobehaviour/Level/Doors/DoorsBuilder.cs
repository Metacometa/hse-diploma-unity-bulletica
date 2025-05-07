using System;
using System.Collections.Generic;
using UnityEngine;

public enum Directions{ Top, Right, Bottom, Left};

public class DoorsBuilder : MonoBehaviour
{
    private DoorsController doorsController;

    [SerializeField] public float intersectionRadius = 0.5f;
    [SerializeField] public float neighboursIntersectionRadius = 1f;

    [SerializeField] public LayerMask mask;

    public List<Transform> nearbyChambers;

    public List<Transform> hitsLeft;
    public List<Transform> hitsTop;
    public List<Transform> hitsRight;
    public List<Transform> hitsBottom;

    void Awake()
    {
        doorsController = GetComponent<DoorsController>();
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

        foreach (Transform v in doorsController.doors)
        {
            //RaycastHit2D[] hits = Physics2D.CircleCastAll(v.position, intersectionRadius, Vector2.zero, 0, mask);
            Collider2D[] hits = Physics2D.OverlapCircleAll(v.position, neighboursIntersectionRadius, mask);

            foreach (Collider2D hit in hits)
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
        hitsTop = new List<Transform>();
        hitsRight = new List<Transform>();
        hitsBottom = new List<Transform>();
        hitsLeft = new List<Transform>();
        for (int i = 0; i < doorsController.doors.Count; ++i)
        {

            Collider2D[] hits = Physics2D.OverlapCircleAll(doorsController.doors[i].position, intersectionRadius, mask);
            //RaycastHit2D[] hits = Physics2D.CircleCastAll(doorsController.doors[i].position, intersectionRadius, Vector2.zero, 0, mask);


            int wallsCounter = 0;
            int doorCounter = 0;

            foreach (Collider2D hit in hits)
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

            Debug.Log("DoorBulder: " + wallsCounter + " " + doorCounter);
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

        if (doorsController.doors.Count > 0)
        {
            foreach (Transform pos in doorsController.doors)
            {
                Gizmos.DrawWireSphere(pos.position, intersectionRadius);
            }
        }

    }
}
