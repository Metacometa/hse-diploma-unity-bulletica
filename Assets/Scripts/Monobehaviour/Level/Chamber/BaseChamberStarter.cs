using UnityEngine;
using UnityEngine.Events;

public class BaseChamberStarter : MonoBehaviour
{
    protected Chamber chamber;
    protected DoorsController doorsController;
    protected EnemyController enemyController;
    protected LightController lightController;

    protected MusicManager musicManager;

    [SerializeField] protected string targetTag;

    public UnityEvent startChamberEvent;

    [Range(0f, 1f)]
    [SerializeField] public float alarmProbability = 1f;

    //[Space]
    public AudioSource audioSource;

    protected virtual void Awake()
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
            startChamberEvent.AddListener(musicManager.PlayFightingPlaylist);
        }

        startChamberEvent.AddListener(onStartSound);

        AddLightListeners();

        //Create audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicManager.soundParameters.chamberStartClip;
        audioSource.playOnAwake = false;
    }

    protected virtual void Start()
    {
        AddLightControllerListener();
    }

    protected virtual void AddLightControllerListener()
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

    protected virtual void AddLightListeners()
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

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        if (targetTag == "") { return; };

        if (collision.CompareTag(targetTag))
        {
            startChamberEvent?.Invoke();
            startChamberEvent?.RemoveAllListeners();
        }
    }

    protected virtual void onStartSound()
    {
        if (audioSource && musicManager)
        {
            Debug.Log($"OnStartSound: {gameObject.name}");
            audioSource.volume = musicManager.soundParameters.volume;

            audioSource.pitch = musicManager.soundParameters.onStartSoundPitch;

            float pitchDiff = musicManager.soundParameters.onStartSoundPitchDiff;
            musicManager.soundParameters.onStartSoundPitch += pitchDiff;

            audioSource.Play();
        }
    }

    protected virtual void OnDestroy()
    {
        startChamberEvent?.RemoveAllListeners();
    }
}
