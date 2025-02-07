using System.Collections;
using TMPro;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class BaseShooting : MonoBehaviour, IShootable
{
    private Transform gun;
    private Transform muzzle;

    public BaseGun source;

    [SerializeField] private float bulletsInMagazine;
    private float magazineCapacity;

    [SerializeField] private GameObject bullet;

    [HideInInspector] public bool onReload;
    [HideInInspector] public bool onCooldown;
    [HideInInspector] public bool onAttack;

    void Start()
    {
        gun = transform.GetChild(0);
        muzzle = gun.transform.GetChild(0);

        magazineCapacity = source.getMagazineCapacity();
        source.LoadGun(ref bulletsInMagazine, ref magazineCapacity);
    }

    public void ShootingManager(in Vector2 to)
    {
        source.Shoot(bullet, muzzle.transform.position, to, ref bulletsInMagazine);

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

        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
}
