using System.Collections.Generic;
using UnityEngine;

public class BulletPoolingManager : MonoBehaviour
{
    [SerializeField] private GameObject[] bulletTypes;

    private SortedDictionary<string, int> order;

    [SerializeField] private GameObject test;

    public List<BulletPooler> poolers;

    [SerializeField] private int numberOfBullets;

    void Awake()
    {
        order = new SortedDictionary<string, int>();
        poolers = new List<BulletPooler>();

        for (int i = 0; i < bulletTypes.Length; i++)
        {
            string category = GetBulletCategory(bulletTypes[i]);
            order.Add(category, i);
        }

        CreateObjectPoolers();
        FillObjectPoolers();
    }

    void Update()
    {
/*        if (test)
        {
            string category = GetBulletCategory(test);
            if (order.ContainsKey(category))
            {
                Debug.Log($"Order: {order[category]}");
            }
            else
            {
                Debug.Log($"Order: null");
            }
        }*/
    }

    public GameObject EnableFromPooler(GameObject bulletGameObject, Vector3 position)
    {
        string category = GetBulletCategory(bulletGameObject);

        if (order.Count > 0 && order.ContainsKey(category))
        {
            int poolerOrder = order[category];
            return poolers[poolerOrder].EnableBullet(position);
        }
        else
        {
            return null;
        }
    }

    void FillObjectPoolers()
    {
        foreach (GameObject bulletType in bulletTypes)
        {
            string category = GetBulletCategory(bulletType);

            if (order.ContainsKey(category))
            {
                BulletPooler pooler = poolers[order[category]];

                int maxNumber = GetBulletMaxNumber(bulletType);
                for (int i = 0; i < maxNumber; i++)
                {
                    GameObject bullet = Instantiate(bulletType, poolers[order[category]].transform);
                    bullet.SetActive(false);
                }

                pooler.maxBullets = maxNumber;
            }
        }
    }

    void CreateObjectPoolers()
    {
        for (int i = 0; i < bulletTypes.Length; ++i)
        {
            GameObject newPooler = new GameObject("ObjectPooler");
            newPooler.transform.parent = transform;

            poolers.Add(newPooler.AddComponent<BulletPooler>());
        }
    }

    string GetBulletCategory(GameObject bull)
    {
        BaseBullet bullComp = bull.GetComponent<BaseBullet>();

        if (bullComp)
        {
            return bullComp.category;
        }

        return "null";
    }    

    int GetBulletMaxNumber(GameObject bull)
    {
        BaseBullet bullComp = bull.GetComponent<BaseBullet>();

        if (bullComp)
        {
            return bullComp.maxNumber;
        }

        return 0;
    }
}

