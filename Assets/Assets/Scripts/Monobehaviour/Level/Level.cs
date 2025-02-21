using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    public bool opening;
    
    void Start()
    {
        ChamberDoors[] doors = GetComponentsInChildren<ChamberDoors>();

        foreach (ChamberDoors c in doors)
        {
            c.WallToDoors();
        }

        opening = true;
    }

    private void Update()
    {
/*        foreach (ChamberBuilder c in chambers)
        {
            if (opening)
            {
                c.OpenAllDoors();
            }
            else
            {
                c.CloseAllDoors();
            }
        }*/
    }
}
