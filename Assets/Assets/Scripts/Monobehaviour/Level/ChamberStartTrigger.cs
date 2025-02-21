using System.Data;
using UnityEngine;

public class ChamberStartTrigger : MonoBehaviour
{
    public ChamberDoors doors;

    [SerializeField] private string triggerTag;

    void Start()
    {
        Transform parent = transform.parent;

        doors = parent.GetComponentInChildren<ChamberDoors>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            doors.CloseDoors();
            doors.CloseNeighboursDoors();

            Transform parent = transform.parent;
            parent.GetComponent<Chamber>().WakeEnemies();

            Destroy(GetComponent<Collider2D>());
        }
    }
}
