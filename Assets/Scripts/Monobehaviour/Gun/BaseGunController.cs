using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseGunController : MonoBehaviour
{
    private Transform gun;
    public Transform muzzle;
    public Transform bulletSpawn;

    public List<BaseGun> attacks;
    /*[HideInInspector]*/ public BaseGun attack;

    [HideInInspector] public GunAnimator gunAnimator;

    [SerializeField] private float bulletsInMagazine;
    private float magazineCapacity;

    [SerializeField] public GameObject bullet;

    /*[HideInInspector]*/ public bool onReload;
    /*[HideInInspector] */public bool onCooldown;
    /*[HideInInspector]*/ public bool onAttack;

    private IEnumerator shootingCoroutine;
    private IEnumerator cooldownCoroutine;
    private IEnumerator reloadCoroutine;

    void Awake()
    {
        if (attacks.Count > 0)
        {
            attack = attacks[0];
        }
        else
        {
            attack = null;
        }

        gun = transform;
        gunAnimator = GetComponent<GunAnimator>();

        //ChangeGun();
        PrepareGun(gun.gameObject);

        if (attack)
        {
            magazineCapacity = attack.getMagazineCapacity();
            attack.LoadGun(ref bulletsInMagazine, ref magazineCapacity);
        }

    }

    public void Shooting()
    {
        if (attack && !onReload && !onAttack && !onCooldown)
        {
            shootingCoroutine = ShootingTimer();
            StartCoroutine(shootingCoroutine);
        }
    }

    IEnumerator ShootingTimer()
    {
        onAttack = true;

        gunAnimator.PullPosition(attack.aimingSpeed);
        yield return new WaitForSeconds(attack.aimingSpeed);

        attack.Shoot(bullet,
            bulletSpawn.transform.position,
            muzzle.transform.position,
            ref bulletsInMagazine);


        onAttack = false;

        Cooldown();
    }
    public void Cooldown()
    {
        if (attack && !onAttack && !onCooldown)
        {
            cooldownCoroutine = CooldownTimer();
            StartCoroutine(cooldownCoroutine);
        }
    }

    IEnumerator CooldownTimer()
    {
        onCooldown = true;

        gunAnimator.RestorePosition(attack.attackExiting);
        yield return new WaitForSeconds(attack.attackExiting);

        yield return new WaitForSeconds(attack.cooldown);

        onCooldown = false;
    }

    public void Reload()
    {
        if (attack && !onReload && !onAttack)
        {
            reloadCoroutine = ReloadTimer();
            StartCoroutine(reloadCoroutine);
        }
    }

    IEnumerator ReloadTimer()
    {
        onReload = true;
        yield return new WaitForSeconds(attack.reloadSpeed);
        onReload = false;

        attack.LoadGun(ref bulletsInMagazine, ref magazineCapacity);
    }

    public bool IsMagazineEmpty()
    {
        return bulletsInMagazine <= 0;
    }

    public void Refresh()
    {
        if (attack)
        {
            attack.LoadGun(ref bulletsInMagazine, ref magazineCapacity);

            onReload = false;
            onAttack = false;
            onCooldown = false;

            StopCoroutine(ShootingTimer());
            StopCoroutine(CooldownTimer());
            StopCoroutine(ReloadTimer());
        }
    }

    void ChangeGun()
    {
        gun.gameObject.SetActive(false);
        Destroy(gun.gameObject);

        if (attack.gun_object != null)
        {
            GameObject taken_gun = Instantiate(attack.gun_object, transform);

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

    public void SetAttack(in int i)
    {
        if (attacks.Count > i)
        {
            attack = attacks[i];
        }
    }

    public void Drain()
    {
        bulletsInMagazine = 0;
    }
}
