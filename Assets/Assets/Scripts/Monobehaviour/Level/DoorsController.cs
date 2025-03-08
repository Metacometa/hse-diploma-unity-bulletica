using System;
using System.Collections.Generic;
using System.Threading;
using Unity.Collections;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    private DoorsBuilder builder;

    public List<Transform> walls;
    public List<Transform> doors;

    public Transform enemies;

    private void Awake()
    {
        builder = GetComponent<DoorsBuilder>();

        int rotationIndex = (int)transform.parent.localRotation.eulerAngles.z / 90;
        builder.ShiftList(ref doors, rotationIndex);
        builder.ShiftList(ref walls, rotationIndex);

        foreach (Directions d in (Directions[])Enum.GetValues(typeof(Directions)))
        {
            doors[(int)d].name = "DoorController" + Enum.GetName(d.GetType(), d);
        }

        foreach (Directions d in (Directions[])Enum.GetValues(typeof(Directions)))
        {
            walls[(int)d].name = "Wall" + Enum.GetName(d.GetType(), d);
        }
    }

    void Start()
    {
        OpenDoors();
    }

    public void OpenNeighboursDoors()
    {
        foreach(Transform d in builder.GetDoorControllers())
        {
            d.GetComponent<Door>().opening = true;
        }
    }

    public void CloseNeighboursDoors()
    {
        foreach (Transform d in builder.GetDoorControllers())
        {
            d.GetComponent<Door>().opening = false;
        }
    }

    public void OpenDoors()
    {
        foreach(Transform door in doors)
        {
            door.GetComponent<Door>().opening = true;
        }
    }

    public void CloseDoors()
    {
        foreach (Transform door in doors)
        {
            door.GetComponent<Door>().opening = false;
        }
    }

    public void WallToDoors()
    {
        builder.WallsToDoors(ref walls, ref doors);
    }
}
