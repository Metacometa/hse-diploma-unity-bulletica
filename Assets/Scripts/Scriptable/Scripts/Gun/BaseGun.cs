using UnityEngine;
using System.Collections.Generic;

public abstract class BaseGun : ScriptableObject
{
    [Header("Basis")]
    public string weapon_name;
    public GameObject gun_object;
    public GameObject bulletObject;

    [Header("Reload")]
    public float reloadSpeed;
    public float rotationSpeed;

    [Header("Attack")]
    public float aimingSpeed;
    public float attackExiting;
    public float cooldown;

    [Header("Spread")]
    public float maxSpreadAngle;

    public abstract void Shoot(in GameObject bullet, in Vector2 from, in Vector2 to, ref float bulletsInMagazine);
    public abstract void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity);
    public abstract int getMagazineCapacity();
}
