using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public bool EnemyRemained()
    {
        return transform.childCount != 0;
    }

    public void EnableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            Transform enemy = transform.GetChild(i);

            transform.GetChild(i).gameObject.SetActive(true);
            enemy.GetComponent<BaseSleep>().Wake();

        }
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
}
