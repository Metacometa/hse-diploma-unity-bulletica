using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseShooting : MonoBehaviour, IShootable
{
    private Transform gun;
    public Transform muzzle;
    public Transform bulletSpawn;

    public BaseGun source;

    [SerializeField] private float bulletsInMagazine;
    private float magazineCapacity;

    [SerializeField] private GameObject bullet;

    [HideInInspector] public bool onReload;
    [HideInInspector] public bool onCooldown;

    void Start()
    {
        gun = GetComponentInChildren<GunJointer>().transform;

        //ChangeGun();
        PrepareGun(gun.gameObject);

        magazineCapacity = source.getMagazineCapacity();
        source.LoadGun(ref bulletsInMagazine, ref magazineCapacity);
    }

    void ChangeGun()
    {
        gun.gameObject.SetActive(false);
        Destroy(gun.gameObject);

        if (source.gun_object != null)
        {
            GameObject taken_gun = Instantiate(source.gun_object, transform);

            gun = taken_gun.transform;
        }
    }

    void PrepareGun(in GameObject gun_)
    {
        Transform point = gun_.transform.Find("Point");

        if (point != null)
        {
            gun = gun_.transform;
            bulletSpawn = point.transform.Find("BulletSpawn");
            muzzle = point.transform.Find("Muzzle");
        }
    }

    public void ShootingManager()
    {
        source.Shoot(bullet, bulletSpawn.transform.position, muzzle.transform.position, ref bulletsInMagazine);

        StartCoroutine(CooldownTimer());
    }
    public IEnumerator CooldownTimer()
    {
        onCooldown = true;
        yield return new WaitForSeconds(source.cooldown);
        onCooldown = false;
    }
    public IEnumerator AttackingTimer()
    {
        onReload = true;
        yield return new WaitForSeconds(source.aimingSpeed);
        onReload = false;

        yield return StartCoroutine(CooldownTimer());
    }

    public void ReloadManager()
    {
        StartCoroutine(ReloadTimer());
    }

    public IEnumerator ReloadTimer()
    {
        onReload = true;
        yield return new WaitForSeconds(source.reloadSpeed);
        onReload = false;

        source.LoadGun(ref bulletsInMagazine, ref magazineCapacity);
    }

    public bool IsMagazineEmpty()
    {
        return bulletsInMagazine <= 0;
    }

    public void RotateGun(in Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);// Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, source.rotationSpeed * Time.deltaTime);
    }

    public void RotateGunInstantly(in Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);// Quaternion.Euler(new Vector3(0, 0, angle));
        transform.rotation = to;
    }
}
