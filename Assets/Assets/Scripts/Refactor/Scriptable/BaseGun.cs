using UnityEngine;
using System.Collections.Generic;

public abstract class BaseGun : ScriptableObject
{
    public string weapon_name;
    public float reloadSpeed;

    public float aimingSpeed;
    public float attackExiting;
    public float cooldown;

    public abstract void Shoot(in GameObject bullet, in Vector2 from, in Vector2 to, ref float bulletsInMagazine);
    public abstract void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity);
    public abstract int getMagazineCapacity();
}
