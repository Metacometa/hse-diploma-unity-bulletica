using System.Collections;
using System.Data;
using UnityEngine;

public class BaseChamberStarter : MonoBehaviour
{
    private Chamber chamber;

    [SerializeField] private string targetTag;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Destroy(GetComponent<Collider2D>());
            chamber.StartChamber();
        }
    }
}
