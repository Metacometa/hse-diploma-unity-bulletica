using System.Collections;
using System.Data;
using UnityEngine;

public class BaseChamberEnder : MonoBehaviour
{
    private Chamber chamber;

    [SerializeField] private string targetTag;

    private EnemyController enemyController;

    void Awake()
    {
        chamber = GetComponentInParent<Chamber>();
        enemyController = chamber.GetComponentInChildren<EnemyController>();
    }

    void Update()
    {
        if (!enemyController.EnemyRemained())
        {
            chamber.FinishChamber();
            gameObject.SetActive(false);
        }
    }
}
