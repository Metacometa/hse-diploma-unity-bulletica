using UnityEngine;
using UnityEngine.Events;

public class BaseChamberEnder : MonoBehaviour
{
    [SerializeField] protected string targetTag;

    protected Chamber chamber;
    protected EnemyController enemyController;
    protected DoorsController doorsController;

    protected MusicManager musicManager;

    [HideInInspector] public UnityEvent endChamberEvent;

    public AudioSource audioSource;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();
        doorsController = chamber.GetComponentInChildren<DoorsController>();

        musicManager = GetComponentInParent<MusicManager>();

        if (doorsController)
        {
            endChamberEvent.AddListener(doorsController.OpenDoors);
            endChamberEvent.AddListener(doorsController.OpenNeighboursDoors);
        }

        if (musicManager)
        {
            endChamberEvent.AddListener(musicManager.PlayAmbientPlaylist);
        }

        endChamberEvent.AddListener(onEndSound);

        AddAlarmListeners();

        //Create audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicManager.soundParameters.chamberEndClip;
        audioSource.playOnAwake = false;
    }

    protected void Start()
    {
        AddLightControllerListener();
    }

    protected void AddAlarmListeners()
    {
        AlarmLight alarm = transform.parent.GetComponentInChildren<AlarmLight>();

        if (alarm)
        {
            endChamberEvent.AddListener(alarm.StopAlarm);
        }
    }

    protected void AddLightControllerListener()
    {
        Level level = GetComponentInParent<Level>();

        if (level)
        {
            LightController[] lightControllers = level.transform.GetComponentsInChildren<LightController>();
            foreach (LightController lightController in lightControllers)
            {
                endChamberEvent.AddListener(lightController.TurnOnDoorLight);
            }
        }
    }

    protected virtual void onEndSound()
    {
        if (audioSource && musicManager)
        {
            audioSource.volume = musicManager.soundParameters.volume;

            //audioSource.pitch = musicManager.soundParameters.onStartSoundPitch;

            //float pitchDiff = musicManager.soundParameters.onStartSoundPitchDiff;
            //musicManager.soundParameters.onStartSoundPitch += pitchDiff;

            audioSource.Play();
        }
    }

    protected void Update()
    {
        if (!enemyController.EnemyRemained())
        {
            endChamberEvent?.Invoke();
            endChamberEvent?.RemoveAllListeners();

            //gameObject.SetActive(false);
        }
    }
}
