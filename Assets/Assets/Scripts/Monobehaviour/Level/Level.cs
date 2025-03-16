using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameParameters gameParameters;

    private void Start()
    {
        foreach (Chamber chamber in GetComponentsInChildren<Chamber>())
        {
            chamber.InitializeRotation();
        }

        WallsToDoors();

        foreach (Chamber chamber in GetComponentsInChildren<Chamber>())
        {
            chamber.transform.GetComponentInChildren<DoorsController>().OpenDoors();
        }
    }

    public void WallsToDoors()
    {
        foreach(DoorsController doorsController in GetComponentsInChildren<DoorsController>())
        {
            doorsController.WallsToDoors();
        }
    }
}
