using UnityEngine;

[CreateAssetMenu(fileName = "EnemyProfile", menuName = "Scriptable Objects/Enemy")]
public class EnemyProfile : ScriptableObject
{
    [SerializeField] public string enemyName;

    [SerializeField] public float shootingRange;
    [SerializeField] public float minDistance;
    [SerializeField] public float sight;

    [SerializeField] public bool shootingOnTheMove;

    [SerializeField] public float moveSpeed;
}
