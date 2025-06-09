using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Events;

public class PlayerDeath : BaseDeath
{
    public UIScreenManager uiManager;
    public UnityEvent playerDeathEvent;

    private Component[] componentsToOff;

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Failure Screen")?.GetComponentInChildren<UIScreenManager>();

        if (uiManager)
        {
            playerDeathEvent.AddListener(uiManager.ShowUI);
        }


        componentsToOff = GetComponents(typeof(Component));

        musicManager = GetComponentInParent<MusicManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicManager.soundParameters.playerDeath;
        audioSource.playOnAwake = false;
    }

    public override void Die(GameObject gameObject)
    {
        gameObject.SetActive(false);

        playerDeathEvent?.Invoke();
    }
}
