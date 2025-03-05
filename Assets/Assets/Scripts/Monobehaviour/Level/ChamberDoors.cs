using System.Collections.Generic;
using UnityEngine;



public class ChamberDoors : MonoBehaviour
{
    private ChamberDoorsHelper helper;

    public List<Transform> walls;
    public List<Transform> doors;

    public Transform enemies;

    private Dictionary<Directions, Transform> doorsTransforms;

    public List<Transform> test;

    private void Awake()
    {
        doorsTransforms = new Dictionary<Directions, Transform>();
        test = new List<Transform>();

        test.Add(null);
        test.Add(null);
        test.Add(null);
        test.Add(null);

        doorsTransforms[Directions.Top] = doors[0];
        doorsTransforms[Directions.Right] = doors[0];
        doorsTransforms[Directions.Bottom] = doors[0];
        doorsTransforms[Directions.Left] = doors[0];



        helper = GetComponent<ChamberDoorsHelper>();
    }

    void Start()
    {




        //helper.GetWalls(ref walls);
        //helper.GetDoors(ref doors);
        //helper.GetPoints(ref doors);

        OpenDoors();
    }

    private void Update()
    {
        test[0] = doorsTransforms[Directions.Top];
        test[1] = doorsTransforms[Directions.Right];
        test[2] = doorsTransforms[Directions.Bottom];
        test[3] = doorsTransforms[Directions.Left];
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
