using UnityEngine;
using static UnityEditor.PlayerSettings;

public class BaseSpawnArea : MonoBehaviour
{
    [SerializeField] public float areaRadius;

    public void SpawnInArea()
    {
        transform.position = transform.position + (Vector3)Random.insideUnitCircle * areaRadius;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, areaRadius);
    }
}
