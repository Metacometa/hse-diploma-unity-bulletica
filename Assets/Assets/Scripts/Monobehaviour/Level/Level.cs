using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager;
using UnityEngine;

public class Level : MonoBehaviour
{
    public GameParameters gameParameters;

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
    }

}
