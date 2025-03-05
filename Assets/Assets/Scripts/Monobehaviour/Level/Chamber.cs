using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Chamber : MonoBehaviour
{
    private ChamberDoors doors;
    public Transform enemies;

    [SerializeField] public float chamberStartTimer;

    public List<Transform> enemiesTransforms;


    void Awake()
    {
        enemiesTransforms = new List<Transform>();



        doors = GetComponentInChildren<ChamberDoors>();
        enemies = transform.Find("Enemies");

        foreach (BaseCharacter enemy in enemies.GetComponentsInChildren<BaseCharacter>())
        {
            enemiesTransforms.Add(enemy.transform);
        }
    }

    void Start()
    {
        SetRandomPosition();
    }

    void SetRandomPosition()
    {
        foreach(Transform t in enemiesTransforms)
        {
            t.gameObject.SetActive(false);

            Transform spawnArea = t.transform.parent;

            SpawnArea spawnAreaComponent = spawnArea.GetComponent<SpawnArea>();

            if (spawnAreaComponent != null)
            {
                t.transform.parent = spawnArea.parent;

                float area = spawnAreaComponent.areaRadius;
                t.transform.position = t.transform.position + (Vector3)Random.insideUnitCircle * area;

                Destroy(spawnArea.gameObject);
            }

        }
    }

    void Update()
    {
        if (enemies != null)
        {
            if (enemies.childCount == 0)
            {
                doors.OpenDoors();
                doors.OpenNeighboursDoors();

                Destroy(enemies.gameObject);
            }
        }
    }

    public void WakeEnemies()
    {
        enemiesTransforms.RemoveAll(element => element == null);

        foreach (Transform t in enemiesTransforms)
        {   
            if (t != null)
            {
                t.gameObject.SetActive(true);
                //t.GetComponent<Enemy>().Wake();
            }
        }
    }
}
