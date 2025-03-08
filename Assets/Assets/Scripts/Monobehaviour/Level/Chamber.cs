using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    private Level level;
    private DoorsController doorsController;
    public Transform enemies;

    [SerializeField] public float chamberStartTimer;

    private EnemyController enemyController;


    void Awake()
    {
        doorsController = GetComponentInChildren<DoorsController>();
        enemyController = GetComponentInChildren<EnemyController>();
        level = GetComponentInParent<Level>();
    }

    void Start()
    {
        doorsController.WallToDoors();

        if (enemyController)
        {
            enemyController.DisableEnemies();
        }
    }

    public void FinishChamber()
    {
        doorsController.OpenDoors();
        doorsController.OpenNeighboursDoors();
    }

    public void StartChamber()
    {
        doorsController.CloseDoors();
        doorsController.CloseNeighboursDoors();

        StartCoroutine(ChamberStartTimer());
    }

    IEnumerator ChamberStartTimer()
    {
        yield return new WaitForSeconds(level.gameParameters.enablingDelay);

        enemyController.EnableEnemies();
    }
}
