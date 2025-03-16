using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class BaseChamberStarter : MonoBehaviour
{
    private Chamber chamber;

    [SerializeField] private string targetTag;

    public UnityEvent chamberStartEvent;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();

        chamberStartEvent.AddListener(Kek);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            Destroy(GetComponent<Collider2D>());
            chamber.StartChamber();

            chamberStartEvent?.Invoke();
        }
    }

    void Kek()
    {
        Debug.Log("Kek");
    }
}
