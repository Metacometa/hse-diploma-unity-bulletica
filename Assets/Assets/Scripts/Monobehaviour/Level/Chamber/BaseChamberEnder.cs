using System.Collections;
using System.Data;
using UnityEngine;
using UnityEngine.Events;

public class BaseChamberEnder : MonoBehaviour
{
    [SerializeField] private string targetTag;

    private Chamber chamber;
    private EnemyController enemyController;
    private DoorsController doorsController;

    [HideInInspector] public UnityEvent endChamberEvent;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();
        doorsController = chamber.GetComponentInChildren<DoorsController>();

        endChamberEvent.AddListener(doorsController.OpenDoors);
        endChamberEvent.AddListener(doorsController.OpenNeighboursDoors);

        AddAlarmListeners();
    }

    private void Start()
    {
        AddLightControllerListener();
    }

    private void AddAlarmListeners()
    {
        AlarmLight alarm = transform.parent.GetComponentInChildren<AlarmLight>();

        if (alarm)
        {
            endChamberEvent.AddListener(alarm.StopAlarm);
        }
    }

    private void AddLightControllerListener()
    {
        Level level = GetComponentInParent<Level>();

        LightController[] lightControllers = level.transform.GetComponentsInChildren<LightController>();
        foreach (LightController lightController in lightControllers)
        {
            endChamberEvent.AddListener(lightController.TurnOnDoorLight);
        }
    }

    void Update()
    {
        if (!enemyController.EnemyRemained())
        {
            endChamberEvent?.Invoke();
            endChamberEvent?.RemoveAllListeners();

            gameObject.SetActive(false);
        }
    }
}
