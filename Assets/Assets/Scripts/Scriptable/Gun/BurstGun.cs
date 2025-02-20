using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "BurstGun", menuName = "Scriptable Objects/Guns/Burst")]
public class BurstGun : BaseGun
{
    [Header("Magazine")]
    [SerializeField] protected int magazineCapacity;

    public override void Shoot(in GameObject bullet, in Vector2 from, in Vector2 to, ref float bulletsInMagazine)
    {
        Vector2 targetDirection = (to - from).normalized;

        GameObject newBullet = Instantiate(bullet, from, Quaternion.identity) as GameObject;
        if (newBullet != null)
        {
            BaseBullet bulletSettings = newBullet.GetComponent<BaseBullet>() as BaseBullet;
            if (bulletSettings != null)
            {
                bulletSettings.ShapeBullet(targetDirection, 0);
            }
        }

        bulletsInMagazine--;
    }

    public override void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity)
    {
        bulletsInMagazine = magazineCapacity;
    }

    public override int getMagazineCapacity()
    {
        return magazineCapacity;
    }
}
