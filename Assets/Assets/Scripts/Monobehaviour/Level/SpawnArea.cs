using UnityEngine;
using static UnityEditor.PlayerSettings;

public class SpawnArea : MonoBehaviour
{
    [SerializeField] public float areaRadius;

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
