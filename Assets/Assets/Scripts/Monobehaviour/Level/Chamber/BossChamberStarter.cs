using UnityEngine;
using UnityEngine.Events;

public class BossChamberStarter : MonoBehaviour
{
    private Chamber chamber;
    private DoorsController doorsController;
    private EnemyController enemyController;
    private LightController lightController;

    private MusicManager musicManager;

    [SerializeField] private string targetTag;

    public UnityEvent startChamberEvent;

    [Range(0f, 1f)]
    [SerializeField] public float alarmProbability = 1f;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        doorsController = chamber.GetComponentInChildren<DoorsController>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();
        lightController = chamber.GetComponentInChildren<LightController>();
        musicManager = GetComponentInParent<MusicManager>();

        if (doorsController)
        {
            startChamberEvent.AddListener(doorsController.CloseDoors);
            startChamberEvent.AddListener(doorsController.CloseNeighboursDoors);
        }

        if (enemyController)
        {
            startChamberEvent.AddListener(enemyController.EnableEnemiesManager);
        }

        if (lightController)
        {
            //startChamberEvent.AddListener(lightController.TurnOnLight);
        }

        if (musicManager)
        {
            startChamberEvent.AddListener(musicManager.PlayBossPlaylist);
        }

        AddLightListeners();
    }

    private void Start()
    {
        AddLightControllerListener();
    }

    private void AddLightControllerListener()
    {
        Level level = GetComponentInParent<Level>();

        if (level)
        {
            LightController[] lightControllers = level.transform.GetComponentsInChildren<LightController>();
            foreach (LightController doorLightController in lightControllers)
            {
                startChamberEvent.AddListener(doorLightController.TurnOffDoorLight);
            }
        }
    }

    private void AddLightListeners()
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
        if (targetTag == "") { return; };

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
