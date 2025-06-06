using UnityEngine;

[CreateAssetMenu(fileName = "BurstGun", menuName = "Scriptable Objects/Guns/Burst")]
public class BurstGun : BaseGun
{
    [Header("Magazine")]
    [SerializeField] protected int magazineCapacity;

    public override void Shoot(in GameObject bullet, in Vector2 from, in Vector2 to, ref float bulletsInMagazine)
    {
        Vector2 targetDirection = (to - from).normalized;

        //GameObject newBullet = Instantiate(bulletObject, from, Quaternion.identity) as GameObject;
        GameObject newBullet = InstantiateFromPooler(from);

        if (newBullet != null)
        {
            BaseBullet bulletSettings = newBullet.GetComponent<BaseBullet>() as BaseBullet;
            if (bulletSettings != null)
            {
                float spreadAngle = Random.Range(-maxSpreadAngle, maxSpreadAngle);
                bulletSettings.ShapeBullet(targetDirection, spreadAngle);
            }
        }

        bulletsInMagazine--;
    }

    private GameObject InstantiateFromPooler(Vector3 position)
    {
        BulletPoolingManager poolManager = FindFirstObjectByType<BulletPoolingManager>();

        if (poolManager)
        {
            return poolManager.EnableFromPooler(bulletObject, position);
            //Debug.Log($"InstantiateFromPooler: { poolManage }");
        }
        else
        {
            return null;
        }
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
