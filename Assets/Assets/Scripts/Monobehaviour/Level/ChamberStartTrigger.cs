using System.Collections;
using System.Data;
using UnityEngine;

public class ChamberStartTrigger : MonoBehaviour
{
    public ChamberDoors doors;
    private Chamber chamber;

    [SerializeField] private string targetTag;

    void Start()
    {
        chamber = transform.parent.GetComponent<Chamber>();

        Transform parent = transform.parent;

        doors = parent.GetComponentInChildren<ChamberDoors>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            doors.CloseDoors();
            doors.CloseNeighboursDoors();

            Destroy(GetComponent<Collider2D>());

            StartCoroutine(ChamberStartTimer());
        }
    }
    IEnumerator ChamberStartTimer()
    {
        yield return new WaitForSeconds(chamber.chamberStartTimer);

        Transform parent = transform.parent;
        parent.GetComponent<Chamber>().WakeEnemies();
    }
}
