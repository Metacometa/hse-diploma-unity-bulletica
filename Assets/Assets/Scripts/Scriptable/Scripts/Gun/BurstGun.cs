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
                float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
                //float spreadAngle = 0;
                bulletSettings.ShapeBullet(targetDirection, spreadAngle);
            }
        }

        bulletsInMagazine--;
    }

    public override void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity)
    {
        magazineCapacity = this.magazineCapacity;
        bulletsInMagazine = magazineCapacity;
    }

    public override int getMagazineCapacity()
    {
        return magazineCapacity;
    }
}
