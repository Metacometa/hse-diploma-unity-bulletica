using UnityEngine;

[CreateAssetMenu(fileName = "BaseWeapon", menuName = "Scriptable Objects/Weapons/BaseWeapon")]
public class BaseWeapon : Weapon
{
    public override void Shoot(in GameObject bullet, in Transform from, in Transform to)
    {
        Vector2 targetDirection = (to.position - from.position).normalized;

        foreach (BulletProperties bulletProps in bullets)
        {
            GameObject newBullet = Instantiate(bullet, from.position, from.rotation) as GameObject;
            if (newBullet != null)
            {
                BulletManager bulletSettings = newBullet.GetComponent<BulletManager>() as BulletManager;
                if (bulletSettings != null)
                {
                    bulletSettings.ShapeBullet(targetDirection, bulletProps.angle, bulletSpeed, bulletForce);
                }
            }
        }
    }
}
