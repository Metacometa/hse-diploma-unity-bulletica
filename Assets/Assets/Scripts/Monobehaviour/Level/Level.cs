using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ChamberBuilder[] chambers;

    public bool opening;
    
    void Start()
    {
        chambers = GetComponentsInChildren<ChamberBuilder>();

        foreach (ChamberBuilder c in chambers)
        {
            c.WallToDoors();
        }

        opening = true;
    }

    private void Update()
    {
        foreach (ChamberBuilder c in chambers)
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
