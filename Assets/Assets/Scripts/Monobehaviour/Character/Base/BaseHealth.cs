using System.Collections;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    public int health;

    void Awake()
    {
        health = GetComponent<BaseCharacter>().profile.health;
    }

    public void TakeDamage(in int damage = 1)
    {
        health = Mathf.Max(0, health - damage);
    }
}
