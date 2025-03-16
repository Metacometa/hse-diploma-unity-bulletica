using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Level level;

    void Awake()
    {
        level = GetComponentInParent<Level>();
    }

    public bool EnemyRemained()
    {
        return transform.childCount != 0;
    }

    public void DisableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enemy = transform.GetChild(i);
            enemy.GetComponent<BaseSleep>().Sleep();
            enemy.gameObject.SetActive(false);
        }
    }

    public void EnableEnemiesManager()
    {
        StartCoroutine(EnableEnemies());
    }

    IEnumerator EnableEnemies()
    {
        yield return new WaitForSeconds(level.gameParameters.enablingEnemiesDelay);

        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enemy = transform.GetChild(i);

            transform.GetChild(i).gameObject.SetActive(true);
            enemy.GetComponent<BaseSleep>().Wake();
        }
    }
}
