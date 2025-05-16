using System.Collections;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth;

    public UIHealthPanelManager healthUI;
    public Level level;

    void Awake()
    {
        health = GetComponent<BaseCharacter>().profile.health;
        maxHealth = health;

        GetHealthUI();
    }

    void GetHealthUI()
    {
        Canvas canvas = FindFirstObjectByType<Canvas>();

        if (canvas)
        {
            //Debug.Log($"Level: {level.transform.parent}");
            healthUI = canvas.GetComponentInChildren<UIHealthPanelManager>();
        }

    }

    public void TakeDamage(in int damage = 1)
    {
        health = Mathf.Max(0, health - damage);

        if (healthUI)
        {
            healthUI.Damage(damage);
        }

    }

    public void Heal(in int damage = 1)
    {
        //health = Mathf.Max(0, health - damage);
    }
}
