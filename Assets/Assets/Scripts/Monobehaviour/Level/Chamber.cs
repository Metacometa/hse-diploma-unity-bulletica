using System.Collections;
using Unity.Cinemachine;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    private Level level;
    private DoorsController doorsController;
    private DoorsBuilder doorsBuilder;
    public Transform enemies;

    [SerializeField] public float chamberStartTimer;

    public EnemyController enemyController;


    void Awake()
    {
        doorsController = GetComponentInChildren<DoorsController>();
        doorsBuilder = GetComponentInChildren<DoorsBuilder>();
        enemyController = GetComponentInChildren<EnemyController>();
        level = GetComponentInParent<Level>();
    }

    void Start()
    {
        if (enemyController)
        {
            enemyController.DisableEnemies();
        }
    }
    
    public void InitializeRotation()
    {
        doorsBuilder.RotatePoints();
        doorsController.RotateWallsAndDoors();

        CinemachineCamera camera = GetComponentInChildren<CinemachineCamera>();

        float newZ = -transform.rotation.eulerAngles.z;
        camera.transform.localRotation = Quaternion.Euler(0, 0, newZ);
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
