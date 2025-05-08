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

        RegenerateCompositeCollider2Ds();

        surface?.BuildNavMesh();
    }

    void RegenerateCompositeCollider2Ds()
    {
        CompositeCollider2D[] compositeCollider2Ds = GetComponentsInChildren<CompositeCollider2D>();
        foreach (CompositeCollider2D compositeCollider2D in compositeCollider2Ds)
        {
            compositeCollider2D.GenerateGeometry();
        }
    }

}
