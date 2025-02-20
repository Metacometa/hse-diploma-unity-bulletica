using System.Data;
using UnityEngine;

public class ChamberStartTrigger : MonoBehaviour
{
    public ChamberBuilder builder;

    [SerializeField] private string triggerTag;

    void Start()
    {
        Transform parent = transform.parent;

        builder = parent.GetComponentInChildren<ChamberBuilder>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(triggerTag))
        {
            builder.CloseDoors();
            builder.CloseNeighboursDoors();

            Destroy(GetComponent<Collider2D>());
        }
    }
}
