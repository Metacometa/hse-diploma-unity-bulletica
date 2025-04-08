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

        float newZ = -transform.localRotation.eulerAngles.z;
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

    public void SetLeftRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    public void SetRightRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 180);
    }

    public void SetBottomRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 90);
    }

    public void SetTopRotation()
    {
        transform.localRotation = Quaternion.Euler(0, 0, 270);
    }

    public Vector3 GetLeftContactPoint()
    {
        return doorsController.doors[0].position;
    }
    public Vector3 GetRightContactPoint()
    {
        return doorsController.doors[1].position;
    }
}
