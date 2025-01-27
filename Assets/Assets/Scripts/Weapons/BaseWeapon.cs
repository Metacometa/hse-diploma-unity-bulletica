using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeapon", menuName = "Scriptable Objects/Weapons/BaseWeapon")]
public class BaseWeapon : Weapon
{
    [Space]
    public List<BulletProperties> bullets;
    [System.Serializable]
    public class BulletProperties
    {
        public float angle;
    }

    public override void Shoot(in GameObject bullet, in Vector2 from, in Vector2 to, ref float bulletsInMagazine)
    {
        Vector2 targetDirection = (to - from).normalized;

        foreach (BulletProperties bulletProps in bullets)
        {
            GameObject newBullet = Instantiate(bullet, from, Quaternion.identity) as GameObject;
            if (newBullet != null)
            {
                BulletManager bulletSettings = newBullet.GetComponent<BulletManager>() as BulletManager;
                if (bulletSettings != null)
                {
                    bulletSettings.ShapeBullet(targetDirection, bulletProps.angle, bulletSpeed, bulletForce);
                }
            }
        }

        bulletsInMagazine -= bullets.Count;
    }

    public override void LoadGun(ref float bulletsInMagazine)
    {
        bulletsInMagazine = bullets.Count;
    }
}
