using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private Chamber[] chambers;

    public bool opening;
    
    void Start()
    {
        chambers = GetComponentsInChildren<Chamber>();

        foreach (Chamber c in chambers)
        {
            c.CheckIntersections();
        }

        opening = true;
    }

    private void Update()
    {
        foreach (Chamber c in chambers)
        {
            if (opening)
            {
                c.OpenAllDoors();
            }
            else
            {
                c.CloseAllDoors();
            }
        }
    }
}
