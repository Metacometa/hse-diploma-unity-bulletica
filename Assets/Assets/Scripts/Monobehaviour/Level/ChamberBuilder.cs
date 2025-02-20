using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

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
        helper.GetPoints();
    }

    private void Update()
    {
        helper.GetPoints();
    }

    public void WallToDoors()
    {
        helper.WallsToDoors(ref walls, ref doors);
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
