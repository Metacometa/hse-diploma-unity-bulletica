using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Scriptable Objects/Profiles/Enemy")]
public class ShootingProfile : BaseProfile
{
    [Header("Shooting")]
    [SerializeField] public float shootingRange;
    [SerializeField] public float approachedDistance;

    [SerializeField] public float minPositionRadius;
    [SerializeField] public float maxPositionRadius;

    [SerializeField] public float changePositionRadius;
    [SerializeField] public float shiftPositionCloser;

    [Space]
    [SerializeField] public bool shootingOnMove;

    [Space]
    public float bulletRadius = 0.5f;
}
