using System.Collections;
using UnityEngine;

public class BaseHealth : MonoBehaviour, IDamageable
{
    public int healthPoints;
    public int startingHealth;

    public void TakeDamage(in int damage = 1)
    {
        healthPoints = Mathf.Max(0, healthPoints - damage);
    }
}
