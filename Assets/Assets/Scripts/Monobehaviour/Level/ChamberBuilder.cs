using System.Collections.Generic;
using UnityEngine;

public class ChamberBuilder : MonoBehaviour
{
    private ChamberBuilderHelper helper;

    public List<Transform> walls;
    public List<Transform> doors;

    void Start()
    {
        helper = GetComponent<ChamberBuilderHelper>();

        helper.GetWalls(ref walls);
        helper.GetDoors(ref doors);
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
