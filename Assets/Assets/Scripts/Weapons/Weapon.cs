using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : ScriptableObject
{
    public string weapon_name;
    public float reloadSpeed;

    public float aimingSpeed;
    public float attackExiting;
    public float cooldown;

    [Header("Bullet")]
    public float bulletSpeed;
    public float bulletForce;

    public abstract void Shoot(in GameObject bullet, in Transform from, in Transform to, ref float bulletsInMagazine);
    public abstract void LoadGun(ref float bulletsInMagazine);
}
