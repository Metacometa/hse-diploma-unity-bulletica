using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Level level;
    [SerializeField] private float additionEnablingDelay;

    BaseCharacter[] enemies;

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
        enemies = GetComponentsInChildren<BaseCharacter>();

        foreach(BaseCharacter enemy in enemies)
        {
           enemy.GetComponent<BaseSleep>().Sleep();
            //enemy.gameObject.SetActive(false);
        }
    }

    public void EnableEnemiesManager()
    {
        StartCoroutine(EnableEnemies());
    }

    IEnumerator EnableEnemies()
    {
        if (level)
        {
            yield return new WaitForSeconds(level.gameParameters.enablingEnemiesDelay + additionEnablingDelay);
        }
        else
        {
            yield return null;
        }

        foreach (BaseCharacter enemy in enemies)
        {
            enemy.GetComponent<BaseSleep>().Wake();
            //enemy.gameObject.SetActive(true);
        }
    }
}
