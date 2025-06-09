using UnityEngine;
using UnityEngine.Events;

public class BossChamberStarter : BaseChamberStarter
{
    protected override void Awake()
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
}
