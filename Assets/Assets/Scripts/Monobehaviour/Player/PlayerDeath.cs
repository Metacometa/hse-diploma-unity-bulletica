using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : BaseDeath
{
    public UIEndScreenManager uiManager;
    public UnityEvent playerDeathEvent;

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("UI")?.GetComponentInChildren<UIEndScreenManager>();

        if (uiManager)
        {
            playerDeathEvent.AddListener(uiManager.ShowUI);
        }
    }

    public override void Die(GameObject gameObject)
    {
        //Destroy(gameObject);
        gameObject.SetActive(false);

        playerDeathEvent?.Invoke();
        Debug.Log("Listeners: " + playerDeathEvent.GetPersistentEventCount());
    }
}
