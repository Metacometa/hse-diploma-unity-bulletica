using NavMeshPlus.Components;
using System.Collections;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameParameters gameParameters;
    public NavMeshSurface surface;
    //public NavigationCollect

    void Awake()
    {
        surface = GetComponentInChildren<NavMeshSurface>();
    }

    void Start()
    {
        StartCoroutine(WallsToDoors());

        foreach (Chamber chamber in GetComponentsInChildren<Chamber>())
        {
            chamber.transform.GetComponentInChildren<DoorsController>().OpenDoors();
        }
    }


    IEnumerator WallsToDoors()
    {
        yield return new WaitForEndOfFrame(); // ∆дем конца кадра
        foreach (DoorsController doorsController in GetComponentsInChildren<DoorsController>())
        {
            doorsController.WallsToDoors();
        }

        surface?.BuildNavMesh();
    }

}
