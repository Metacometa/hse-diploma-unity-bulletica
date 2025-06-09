using UnityEngine;

public class BossChamberEnder : BaseChamberEnder
{
    public UIScreenManager uiManager;

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Victory Screen")?.GetComponentInChildren<UIScreenManager>();

        if (uiManager)
        {
            endChamberEvent.AddListener(uiManager.ShowUI);
        }

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
            endChamberEvent.AddListener(musicManager.PlayWinPlaylist);
        }

        endChamberEvent.AddListener(onEndSound);

        AddAlarmListeners();

        //Create audio source
        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicManager.soundParameters.chamberEndClip;
        audioSource.playOnAwake = false;
    }
}
