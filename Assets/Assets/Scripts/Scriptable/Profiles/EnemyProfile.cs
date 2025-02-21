using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Scriptable Objects/Enemy")]
public class EnemyProfile : ScriptableObject
{
    [SerializeField] public string enemyName;

    [SerializeField] public float shootingRange;
    [SerializeField] public float approachedDistance;
    [SerializeField] public float sight;

    [SerializeField] public bool shootingOnMove;

    [SerializeField] public float moveSpeed;
}
