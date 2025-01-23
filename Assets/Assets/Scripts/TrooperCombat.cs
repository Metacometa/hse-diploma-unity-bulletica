using System.Collections;
using UnityEngine;

public class TrooperCombat : MonoBehaviour
{
    TrooperData data;

    [SerializeField] public float shootingRange;
    [SerializeField] public float minDistance;

    [SerializeField] public float sight;


    [SerializeField] private BaseWeapon weapon;
    [Header("Gun")]
    [SerializeField] public GameObject bullet;

    public float[] angles;

    void Start()
    {
        data = GetComponent<TrooperData>();
    }

/*    public void Shoot()
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
                    bulletSettings.ShapeBullet(targetDirection, angle, weapon.bulletSpeed, weapon.bulletForce);
                }
            }
        }
    }*/
    public IEnumerator ShootManager()
    {
        data.attacking = true;
        yield return new WaitForSeconds(weapon.aimingSpeed);

        weapon.Shoot(bullet, transform, data.target);
        StartCoroutine(Reloading());

        yield return new WaitForSeconds(weapon.attackExiting);

        data.attacking = false;
    }
    public IEnumerator Reloading()
    {
        data.onReload = true;
        yield return new WaitForSeconds(weapon.reloadSpeed);
        data.onReload = false;
    }
}
