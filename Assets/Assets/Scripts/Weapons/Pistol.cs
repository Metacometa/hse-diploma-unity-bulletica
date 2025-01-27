using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OneShotWeapon", menuName = "Scriptable Objects/Weapons/OneShotWeapon")]
public class OneShotWeapon : Weapon
{
    [SerializeField] public float magazineCapacity;

    public override void Shoot(in GameObject bullet, in Transform from, in Transform to, ref float bulletsInMagazine)
    {
        Vector2 targetDirection = (to.position - from.position).normalized;

        GameObject newBullet = Instantiate(bullet, from.position, from.rotation) as GameObject;
        if (newBullet != null)
        {
            BulletManager bulletSettings = newBullet.GetComponent<BulletManager>() as BulletManager;
            if (bulletSettings != null)
            {
                bulletSettings.ShapeBullet(targetDirection, 0, bulletSpeed, bulletForce);
            }
        }

        bulletsInMagazine--;
    }

    public override void LoadGun(ref float bulletsInMagazine)
    {
        bulletsInMagazine = magazineCapacity;
    }
}
