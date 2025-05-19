using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "OneShotGun", menuName = "Scriptable Objects/Guns/OneShot")]
public class OneShotGun : BaseGun
{
    [Header("Magazine")]
    [SerializeField] protected int shots;
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
            GameObject newBullet = Instantiate(bulletObject, from, Quaternion.identity) as GameObject;
            if (newBullet != null)
            {
                BaseBullet bulletSettings = newBullet.GetComponent<BaseBullet>() as BaseBullet;
                if (bulletSettings != null)
                {
                    bulletSettings.ShapeBullet(targetDirection, bulletProps.angle);
                }
            }
        }

        bulletsInMagazine--;
    }

    public override void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity)
    {
        bulletsInMagazine = shots;
        magazineCapacity = shots;
    }

    public override int getMagazineCapacity()
    {
        return shots;
    }
}
