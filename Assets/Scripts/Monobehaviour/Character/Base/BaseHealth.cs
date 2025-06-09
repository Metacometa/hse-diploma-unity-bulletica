using System.Collections;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    public int health;
    public int maxHealth;

    void Awake()
    {
        health = GetComponent<BaseCharacter>().profile.health;
        maxHealth = health;
    }

    public void TakeDamage(in int damage = 1)
    {
        health = Mathf.Max(0, health - damage);
    }

    public void Heal(in int damage = 1)
    {
        //health = Mathf.Max(0, health - damage);
    }
}
