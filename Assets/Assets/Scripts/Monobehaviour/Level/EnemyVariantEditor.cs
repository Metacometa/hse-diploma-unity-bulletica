using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariantEditor : MonoBehaviour
{
    [SerializeField] private GameObject[] groups;
    [SerializeField] private string targetTag;

    [SerializeField] private List<Transform> enemiesTransforms;

    private void Awake()
    {
        enemiesTransforms = new List<Transform>();

        foreach (SimpleEnemy enemy in GetComponentsInChildren<SimpleEnemy>())
        {
            enemiesTransforms.Add(enemy.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(targetTag))
        {
            foreach (GameObject gameObject in groups)
            {
                Destroy(gameObject);
            }

            Destroy(GetComponent<Collider2D>());

            foreach (Transform t in enemiesTransforms)
            {
                t.parent = t.parent.parent;
            }

            Destroy(gameObject);
        }
    }
}
