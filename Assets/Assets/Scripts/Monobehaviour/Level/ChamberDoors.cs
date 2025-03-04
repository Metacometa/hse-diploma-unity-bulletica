using System.Collections.Generic;
using UnityEngine;

public class ChamberDoors : MonoBehaviour
{
    private ChamberDoorsHelper helper;

    public List<Transform> walls;
    public List<Transform> doors;

    public Transform enemies;

    void Start()
    {
        helper = GetComponent<ChamberDoorsHelper>();

        //helper.GetWalls(ref walls);
        //helper.GetDoors(ref doors);
        //helper.GetPoints(ref doors);

        OpenDoors();
    }

    public void OpenNeighboursDoors()
    {
        foreach(Transform d in helper.GetDoorControllers())
        {
            d.GetComponent<Door>().opening = true;
        }
    }

    public void CloseNeighboursDoors()
    {
        foreach (Transform d in helper.GetDoorControllers())
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
    public void GetNearbyChambers()
    {
        helper.GetNearbyChambers();
    }

    public void WallToDoors()
    {
        helper.WallsToDoors(ref walls, ref doors);
    }
}
