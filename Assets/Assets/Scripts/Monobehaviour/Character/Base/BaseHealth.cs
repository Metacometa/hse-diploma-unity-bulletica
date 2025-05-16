using System.Collections;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth;

    private UIHealthPanelManager healthUI;

    void Awake()
    {
        health = GetComponent<BaseCharacter>().profile.health;
        maxHealth = health;

        GetHealthUI();
    }

    void GetHealthUI()
    {
        Level level = GetComponentInParent<Level>();

        healthUI = level.transform.parent.GetComponentInChildren<UIHealthPanelManager>();
    }

    public void TakeDamage(in int damage = 1)
    {
        health = Mathf.Max(0, health - damage);

        healthUI.Damage(damage);
    }

    public void Heal(in int damage = 1)
    {
        //health = Mathf.Max(0, health - damage);
    }
}
