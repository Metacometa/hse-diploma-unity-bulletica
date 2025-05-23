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

    [Header("Gun")]
    [SerializeField] public float rotationSpeed;
    [SerializeField] public float gunRotationSpeed;

    [SerializeField] public float gunRotateCooldown;
    [SerializeField] public float tankRotateCooldown;

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

    [Header("Shimmer")]
    [SerializeField] public int shimmerTime;
    [SerializeField] public Color shimmerColor;

    [Header("Helper")]
    [SerializeField] public float shootingRangeShift;
    [SerializeField] public float predictionDepth;
}
