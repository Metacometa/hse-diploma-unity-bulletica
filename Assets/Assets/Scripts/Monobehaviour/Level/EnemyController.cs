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
            transform.GetChild(i).gameObject.SetActive(true);
        }
    }

    public void DisableEnemies()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
