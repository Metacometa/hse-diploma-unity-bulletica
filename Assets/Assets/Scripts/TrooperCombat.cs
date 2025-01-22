using System.Collections;
using UnityEngine;

public class TrooperCombat : MonoBehaviour
{
    TrooperData data;

    [SerializeField] public float shootingRange;
    [SerializeField] public float minDistance;
    [SerializeField] public float reloadSpeed;

    [SerializeField] public float attackPreparing;
    [SerializeField] public float attackExiting;

    [SerializeField] public float sight;

    [Header("Gun")]
    [SerializeField] public GameObject bullet;
    [SerializeField] public float bulletSpeed;
    [SerializeField] public float bulletForce;

    public float[] angles;

    void Start()
    {
        data = GetComponent<TrooperData>();
    }

    public void Shoot()
    {
        Vector2 targetDirection = (data.target.position - transform.position).normalized;

        foreach (float angle in angles)
        {
            GameObject newBullet = Instantiate(bullet, transform.position, transform.rotation) as GameObject;
            if (newBullet != null)
            {
                BulletManager bulletSettings = newBullet.GetComponent<BulletManager>() as BulletManager;
                if (bulletSettings != null)
                {
                    bulletSettings.ShapeBullet(targetDirection, angle, bulletSpeed, bulletForce);
                }
            }
        }
    }

    public IEnumerator ShootManager()
    {
        data.attacking = true;
        yield return new WaitForSeconds(attackPreparing);

        Shoot();
        StartCoroutine(Reloading());

        yield return new WaitForSeconds(attackExiting);

        data.attacking = false;
    }
    public IEnumerator Reloading()
    {
        data.onReload = true;
        yield return new WaitForSeconds(reloadSpeed);
        data.onReload = false;
    }
}
