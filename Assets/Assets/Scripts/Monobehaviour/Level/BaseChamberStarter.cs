using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class BaseChamberStarter : MonoBehaviour
{
    private Chamber chamber;
    private DoorsController doorsController;
    private EnemyController enemyController;

    [SerializeField] private string targetTag;

    [HideInInspector] public UnityEvent startChamberEvent;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        doorsController = chamber.GetComponentInChildren<DoorsController>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();

        startChamberEvent.AddListener(doorsController.CloseDoors);
        startChamberEvent.AddListener(doorsController.CloseNeighboursDoors);

        startChamberEvent.AddListener(enemyController.EnableEnemiesManager);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            startChamberEvent?.Invoke();
            startChamberEvent?.RemoveAllListeners();
        }
    }

    void OnDestroy()
    {
        startChamberEvent?.RemoveAllListeners();
    }
}
