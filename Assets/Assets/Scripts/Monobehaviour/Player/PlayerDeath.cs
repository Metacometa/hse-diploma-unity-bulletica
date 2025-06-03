using UnityEngine;
using UnityEngine.Events;

public class PlayerDeath : BaseDeath
{
    public UIScreenManager uiManager;
    public UnityEvent playerDeathEvent;

    void Awake()
    {
        uiManager = GameObject.FindGameObjectWithTag("Failure Screen")?.GetComponentInChildren<UIScreenManager>();

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
