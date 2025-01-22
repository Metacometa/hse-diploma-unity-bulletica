using Unity.VisualScripting;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public Transform player;

    [SerializeField] private GameObject heartPrefab;

    private float drawnHearts = 0;

    void Update()
    {
        if (player != null)
        {
            int playerHealth = player.GetComponent<PlayerHealth>().healths;

            while (playerHealth != drawnHearts)
            {
                DrawHearts(playerHealth);
            }
        }
    }

    void AddHeart()
    {
        GameObject newHeart = Instantiate(heartPrefab);
        if (newHeart != null)
        {
            newHeart.transform.SetParent(transform);
        }
    }

    void DeleteHeart()
    {
        int children = transform.childCount;
        if (children > 0)
        {
            Transform child = transform.GetChild(children - 1);

            if (child != null)
            {
                Destroy(child.gameObject);
            }
        }
    }

    void DrawHearts(in int playerHealth)
    {
        if (playerHealth > drawnHearts)
        {
            AddHeart();
            drawnHearts++;
        }
        else if (playerHealth < drawnHearts)
        {
            DeleteHeart();
            drawnHearts--;
        }
    }
}
