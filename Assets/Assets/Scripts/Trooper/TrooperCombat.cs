using System.Collections;
using UnityEngine;

public class TrooperCombat : MonoBehaviour
{
    private TrooperData data;
    private GunManager gun;

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

        StartCoroutine(gun.Cooldown());
    }
}
