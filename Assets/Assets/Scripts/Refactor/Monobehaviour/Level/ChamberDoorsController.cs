using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChamberDoorsController : MonoBehaviour
{
    private ChamberDoorsManager manager;

    public List<Transform> walls;
    public List<Transform> doors;

    void Start()
    {
        manager = GetComponent<ChamberDoorsManager>();

        manager.GetWalls(ref walls);
        manager.GetDoors(ref doors);
        manager.GetPoints();
    }

    public void WallToDoors()
    {
        manager.WallsToDoors(ref walls, ref doors);
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
}
