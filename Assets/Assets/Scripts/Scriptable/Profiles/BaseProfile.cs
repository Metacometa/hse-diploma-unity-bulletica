using UnityEngine;

public class BaseProfile : ScriptableObject
{
    [SerializeField] public string enemyName;
    [SerializeField] public float moveSpeed;

    [SerializeField] public float sight;
}
