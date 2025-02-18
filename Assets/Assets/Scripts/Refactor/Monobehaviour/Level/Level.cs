using System.Collections.Generic;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private ChamberDoorsController[] chambers;

    public bool opening;
    
    void Start()
    {
        chambers = GetComponentsInChildren<ChamberDoorsController>();

        foreach (ChamberDoorsController c in chambers)
        {
            c.WallToDoors();
        }

        opening = true;
    }

    private void Update()
    {
        foreach (ChamberDoorsController c in chambers)
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
