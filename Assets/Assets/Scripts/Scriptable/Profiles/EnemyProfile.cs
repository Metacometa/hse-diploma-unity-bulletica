using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Scriptable Objects/Profiles/Enemy")]
public class EnemyProfile : BaseProfile
{
    [Header("Shooting")]
    [SerializeField] public float shootingRange;
    [SerializeField] public float approachedDistance;

    [Space]
    [SerializeField] public bool shootingOnMove;
}
