using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OneShotGun", menuName = "Scriptable Objects/Guns/OneShot")]
public class OneShotGun : BaseGun
{
    [Space]

    public List<BulletProperties> bullets;

    [System.Serializable] public class BulletProperties
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
                BaseBullet bulletSettings = newBullet.GetComponent<BaseBullet>() as BaseBullet;
                if (bulletSettings != null)
                {
                    bulletSettings.ShapeBullet(targetDirection, bulletProps.angle);
                }
            }
        }

        bulletsInMagazine = 0;
    }

    public override void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity)
    {
        bulletsInMagazine = bullets.Count;
        magazineCapacity = bullets.Count;
    }

    public override int getMagazineCapacity()
    {
        return bullets.Count;
    }
}
