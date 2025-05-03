using UnityEngine;
using UnityEngine.UI;

public class BaseProfile : ScriptableObject
{
    [SerializeField] public string enemyName;

    [Header("Moving")]
    [SerializeField] public float moveSpeed;
    [SerializeField] public float pushingAwayTime;
    [SerializeField] public float pushingAwayForce;

    [Space]
    [SerializeField] public float stayBufferTime;

    [Space]
    [SerializeField] public float sightRange;
    [SerializeField] public float pursueRange;

    [Space]
    [SerializeField] public int health;

    [Space]
    [SerializeField] public float invincibilityTime;

    [Space]
    [SerializeField] public string targetTag;

    [Header("Masks")] 
    [SerializeField] public LayerMask sightMask;
    [SerializeField] public LayerMask pursueMask;
    [SerializeField] public LayerMask changePositionMask;
    [SerializeField] public LayerMask shootingMask;
}
