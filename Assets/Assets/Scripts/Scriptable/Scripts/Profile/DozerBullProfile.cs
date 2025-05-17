using UnityEngine;

[CreateAssetMenu(fileName = "DozerBull", menuName = "Scriptable Objects/Profiles/Bosses/DozerBullProfile")]
public class DozerBullProfile : ScriptableObject
{
    [Header("Breakthrough")]
    [SerializeField] public float breakthroughCooldown;
    [SerializeField] public LayerMask breakthroughMask;
    [SerializeField] public float attackRange;
    [SerializeField] public float breakthroughSpeed;

}
