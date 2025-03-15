using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameParameters gameParameters;

    private void Start()
    {
        foreach (Chamber chanber in GetComponentsInChildren<Chamber>())
        {
            chanber.InitializeRotation();
        }

        WallsToDoors();

        foreach (Chamber chanber in GetComponentsInChildren<Chamber>())
        {
            chanber.transform.GetComponentInChildren<DoorsController>().OpenDoors();
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
