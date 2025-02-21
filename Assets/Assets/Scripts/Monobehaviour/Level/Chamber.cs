using UnityEngine;

public class Chamber : MonoBehaviour
{
    private ChamberDoors doors;
    public Transform enemies;

    void Start()
    {
        doors = GetComponentInChildren<ChamberDoors>();
        enemies = transform.Find("Enemies");
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
        foreach (Enemy e in enemies.GetComponentsInChildren<Enemy>())
        {
            e.Wake();
        }
    }
}
