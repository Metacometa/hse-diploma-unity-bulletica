using UnityEngine;

public class BaseProfile : ScriptableObject
{
    [SerializeField] public string enemyName;

    [Header("Moving")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float pushingAwayTime;
    [SerializeField] public float pushingAwayForce;

    [Space]
    [SerializeField] public float sight;

    [Space]
    [SerializeField] public int health;

    [Space]
    [SerializeField] public float invincibilityTime;

    [Space]
    [SerializeField] public string targetTag;
}
