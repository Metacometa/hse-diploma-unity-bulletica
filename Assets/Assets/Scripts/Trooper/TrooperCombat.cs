using System.Collections;
using UnityEngine;

public class TrooperCombat : MonoBehaviour
{
    private TrooperData data;
    [HideInInspector] public GunManager gun;

    void Start()
    {
        data = GetComponent<TrooperData>();
        gun = GetComponent<GunManager>();
    }

    public IEnumerator ShootManager()
    {
        data.attacking = true;

        yield return new WaitForSeconds(gun.source.aimingSpeed);

        gun.Shoot(data.target.position);

        data.attacking = false;

        StartCoroutine(Cooldown());
    }

    public IEnumerator Cooldown()
    {
        data.onCooldown = true;
        yield return new WaitForSeconds(gun.source.cooldown);

        data.onCooldown = false;
    }

    public IEnumerator Reload()
    {
        data.onReload = true;
        yield return new WaitForSeconds(gun.source.reloadSpeed);

        gun.LoadGun();

        data.onReload = false;
    }

}
