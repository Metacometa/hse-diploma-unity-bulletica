using UnityEngine;

public class BossShooting : MonoBehaviour, IShootable
{
    public BaseGunController[] gunController;
    public BaseGunController currentGun;

    protected bool Switch;

    protected virtual void Awake()
    {
        gunController = GetComponentsInChildren<BaseGunController>();
        currentGun = gunController[0];

        Switch = false;
    }

    public virtual void ShootingManager()
    {
/*        if (Switch)
        {
            currentGun = gunController[0];
        }
        else
        {
            currentGun = gunController[1];
        }    

        currentGun.Shooting();
        Switch = !Switch;*/
    }

    public virtual void ReloadManager()
    {
        currentGun.Reload();
    }

    public virtual bool IsMagazineEmpty()
    {
        if (gunController == null)
        {
            return false;
        }
        return currentGun.IsMagazineEmpty();
    }

    public bool OnReload()
    {
        bool summaryOnReload = true;
        foreach (var gun in gunController)
        {
            summaryOnReload = summaryOnReload && currentGun.onReload;
        }

        return summaryOnReload;
    }

    public bool OnCooldown()
    {
        return currentGun.onCooldown;

        bool summaryOnCooldown = true;
        foreach(var gun in gunController)
        {
            summaryOnCooldown = summaryOnCooldown && gun.onCooldown;
        }

        return summaryOnCooldown;
    }

    public bool OnAttack()
    {
        return currentGun.onAttack;
    }

    public float GetRotationSpeed()
    {
        return currentGun.attack.rotationSpeed;
    }
}
