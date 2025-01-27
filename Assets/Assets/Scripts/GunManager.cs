using UnityEngine;

public class GunManager : MonoBehaviour
{
    private Transform gun;
    private Transform muzzle;
    [SerializeField] public Weapon source;

    [SerializeField] private float bulletsInMagazine;

    [SerializeField] private GameObject bullet;

    private void Start()
    {
        gun = transform.GetChild(0);
        muzzle = gun.transform.GetChild(0);

        source.LoadGun(ref bulletsInMagazine);
    }

    public void rotateToTarget(in Transform target)
    {
        Vector2 dir = target.position - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void rotateToTarget(in Vector2 targetPosition)
    {
        Vector2 dir = targetPosition - (Vector2)transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public bool isMagazineEmpty()
    {
        return bulletsInMagazine <= 0;
    }

    public void Shoot(in Vector2 to)
    {
        source.Shoot(bullet, muzzle.transform.position, to, ref bulletsInMagazine);
    }

    public void LoadGun()
    {
        source.LoadGun(ref bulletsInMagazine);
    }
}
