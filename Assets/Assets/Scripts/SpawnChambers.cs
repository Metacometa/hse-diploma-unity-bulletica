using UnityEngine;

public class SpawnChambers : MonoBehaviour
{
    public GameObject prefab;

    void Awake()
    {
        Chamber room1 = Instantiate(prefab, transform.position, Quaternion.identity, transform.parent).GetComponent<Chamber>();
        //room1.SetBottomRotation();

        Chamber leftRoom = Instantiate(prefab, transform.position + room1.GetLeftContactPoint(), Quaternion.identity, transform.parent).GetComponent<Chamber>();

        Chamber rightRoom = Instantiate(prefab, transform.position + room1.GetRightContactPoint(), Quaternion.identity, transform.parent).GetComponent<Chamber>();

        //Instantiate(prefab, transform.position, Quaternion.identity, transform.parent);

        //Instantiate(prefab, transform.position + new Vector3(0, 14, 0), Quaternion.Euler(0, 0, 90), transform.parent);

        //Instantiate(prefab, transform.position + new Vector3(-16, 0, 0), Quaternion.Euler(0, 0, 180), transform.parent);
    }

}
