using System.Collections;
using UnityEngine;

public class TrooperCombat : MonoBehaviour
{
    TrooperData data;

    [SerializeField] private Weapon weapon;
    [Header("Gun")]
    [SerializeField] public GameObject bullet;

    [SerializeField] public float bulletsInMagazine;

    private GameObject gun;

    void Start()
    {
        data = GetComponent<TrooperData>();
        weapon.LoadGun(ref bulletsInMagazine);

        gun = transform.GetChild(0).gameObject;
    }

    public IEnumerator ShootManager()
    {
        data.attacking = true;

        yield return new WaitForSeconds(weapon.aimingSpeed);

        weapon.Shoot(bullet, transform, data.target, ref bulletsInMagazine);

        data.attacking = false;

        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        data.onCooldown = true;
        yield return new WaitForSeconds(weapon.cooldown);

        data.onCooldown = false;
    }

    public IEnumerator Reload()
    {
        data.onReload = true;
        yield return new WaitForSeconds(weapon.reloadSpeed);

        weapon.LoadGun(ref bulletsInMagazine);

        data.onReload = false;
    }

    public bool isMagazineEmpty()
    {
        return bulletsInMagazine <= 0; 
    }
}
