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

    private GunJointer gunJointer;

    [SerializeField] private float bulletsInMagazine;
    private float magazineCapacity;

    [SerializeField] private GameObject bullet;

    [HideInInspector] public bool onReload;
    [HideInInspector] public bool onCooldown;
    [HideInInspector] public bool onAttack;

    void Awake()
    {
        gun = GetComponentInChildren<GunJointer>().transform;
        gunJointer = gun.GetComponentInChildren<GunJointer>();

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
        float elapsedTime = 0;

        onCooldown = true;
        while (elapsedTime <= source.cooldown)
        {
            elapsedTime += Time.deltaTime;
            
            if (gunJointer != null)
            {
                gunJointer.RestorePosition(elapsedTime, source.cooldown);
            }

            yield return null;
        }
        onCooldown = false;
    }
    public IEnumerator AttackingTimer()
    {
        float elapsedTime = 0;

        onAttack = true;
        while (elapsedTime <= source.aimingSpeed)
        {
            elapsedTime += Time.deltaTime;

            if (gunJointer != null)
            {
                gunJointer.PullPosition(elapsedTime, source.aimingSpeed);
            }

            yield return null;
        }
        onAttack = false;

        source.Shoot(bullet, bulletSpawn.transform.position, muzzle.transform.position, ref bulletsInMagazine);

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
