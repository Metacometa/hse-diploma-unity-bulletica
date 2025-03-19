using UnityEngine;

public class SpawnChambers : MonoBehaviour
{
    public GameObject prefab;

    void Awake()
    {
        Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);

        Instantiate(prefab, transform.position + new Vector3(0, 14, 0), Quaternion.Euler(0, 0, 90), transform.parent);

        Instantiate(prefab, transform.position + new Vector3(-16, 0, 0), Quaternion.Euler(0, 0, 180), transform.parent);
    }

}
