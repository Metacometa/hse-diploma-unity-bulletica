using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour, IDamageable
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
        health = Mathf.Clamp(health - damage, 0, maxHealth);

        if (healthUI)
        {
            healthUI.Damage(damage);
        }

    }

    public void Heal(in int heal = 1)
    {
        health = Mathf.Clamp(health + heal, 0, maxHealth);

        if (healthUI)
        {
            healthUI.Heal(heal);
        }
    }

    public void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("Kek");

        if (collision.transform.CompareTag("HealthPointItem") &&
            PlayerInjured())
        {
            Heal();
        }
    }

    public bool PlayerInjured()
    {
        return health < maxHealth;
    }
}
