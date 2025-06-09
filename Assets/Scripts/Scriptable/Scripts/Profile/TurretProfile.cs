using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "Scriptable Objects/Profiles/Turret")]
public class TurretProfile : ScriptableObject
{
    [SerializeField] public float attackChangeDistance;
    [SerializeField] public LayerMask mask;
}
