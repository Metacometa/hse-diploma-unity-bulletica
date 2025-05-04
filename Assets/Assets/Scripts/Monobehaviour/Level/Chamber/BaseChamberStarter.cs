using System.Collections;
using System.Data;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;

public class BaseChamberStarter : MonoBehaviour
{
    private Chamber chamber;
    private DoorsController doorsController;
    private EnemyController enemyController;

    [SerializeField] private string targetTag;

    [HideInInspector] public UnityEvent startChamberEvent;

    [Range(0f, 1f)]
    [SerializeField] public float alarmProbability = 1f;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        doorsController = chamber.GetComponentInChildren<DoorsController>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();

        startChamberEvent.AddListener(doorsController.CloseDoors);
        startChamberEvent.AddListener(doorsController.CloseNeighboursDoors);

        startChamberEvent.AddListener(enemyController.EnableEnemiesManager);

        AddLightListeners();
    }

    void AddLightListeners()
    {
        AlarmLight alarm = transform.parent.GetComponentInChildren<AlarmLight>();

        float getAlarmProbability = Random.value;

        if (getAlarmProbability <= alarmProbability)
        {
            if (alarm)
            {
                startChamberEvent.AddListener(alarm.StartAlarm);
            }
        }
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
