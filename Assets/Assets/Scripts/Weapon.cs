using UnityEngine;
using System.Collections.Generic;

public abstract class Weapon : ScriptableObject
{
    public string weapon_name;
    public float reloadSpeed;

    public float aimingSpeed;
    public float attackExiting;

    [Header("Bullet")]
    public float bulletSpeed;
    public float bulletForce;

    [Space]
    public List<BulletProperties> bullets;
    [System.Serializable]
    public class BulletProperties
    {
        public float angle;
    }

    public abstract void Shoot(in GameObject bullet, in Transform from, in Transform to);
}
