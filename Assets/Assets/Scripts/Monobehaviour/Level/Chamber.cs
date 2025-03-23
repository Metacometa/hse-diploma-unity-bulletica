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

    private EnemyController enemyController;
    [SerializeField] private GameObject horizontalRoom;
    [SerializeField] private GameObject verticalRoom;

    public CinemachineCamera cameraS;

    void Awake()
    {
        doorsController = GetComponentInChildren<DoorsController>();
        doorsBuilder = GetComponentInChildren<DoorsBuilder>();
        enemyController = GetComponentInChildren<EnemyController>();
        level = GetComponentInParent<Level>();

        horizontalRoom = transform.Find("RoomHorizontal")?.gameObject;
        verticalRoom = transform.Find("RoomVertical")?.gameObject;
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
        DeleteExtraRoom();

        CinemachineCamera camera = GetComponentInChildren<CinemachineCamera>();
        cameraS = camera;

        float newZ = -transform.rotation.eulerAngles.z;
        camera.transform.localRotation = Quaternion.Euler(0, 0, newZ);
    }

    void DeleteExtraRoom()
    {
        float angle = transform.rotation.eulerAngles.z;
        if (angle == 90 || angle == 270)
        {
            DestroyImmediate(horizontalRoom);
        }
        else
        {
            DestroyImmediate(verticalRoom);
        }
    }
}
