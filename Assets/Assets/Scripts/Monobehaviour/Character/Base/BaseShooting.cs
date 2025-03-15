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

    private GunAnimator gunAnimator;

    [SerializeField] private float bulletsInMagazine;
    private float magazineCapacity;

    [SerializeField] private GameObject bullet;

    [HideInInspector] public bool onReload;
    [HideInInspector] public bool onCooldown;
    [HideInInspector] public bool onAttack;

    void Awake()
    {
        gun = GetComponentInChildren<GunAnimator>().transform;
        gunAnimator = gun.GetComponentInChildren<GunAnimator>();

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
        StartCoroutine(AttackingTimer());
    }
    public IEnumerator CooldownTimer()
    {
        onCooldown = true;

        yield return new WaitForSeconds(source.cooldown);

        onCooldown = false;
    }
    public IEnumerator AttackingTimer()
    {
        onAttack = true;

        gunAnimator.PullPosition(source.aimingSpeed);
        yield return new WaitForSeconds(source.aimingSpeed);

        source.Shoot(bullet, bulletSpawn.transform.position, muzzle.transform.position, ref bulletsInMagazine);


        gunAnimator.RestorePosition(source.attackExiting);
        yield return new WaitForSeconds(source.attackExiting);

        onAttack = false;

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
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.RotateTowards(transform.rotation, to, source.rotationSpeed * Time.deltaTime);
    }

    public void RotateGunInstantly(in Vector2 dir)
    {
        float angle = Mathf.Atan2(dir.normalized.y, dir.normalized.x) * Mathf.Rad2Deg;

        Quaternion to = Quaternion.AngleAxis(angle, Vector3.forward);;
        transform.rotation = to;
    }
}
