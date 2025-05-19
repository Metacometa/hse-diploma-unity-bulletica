using UnityEngine;

public class BaseShooting : MonoBehaviour, IShootable
{
    public BaseGunController gunController;

    public virtual void Awake()
    {
        gunController = GetComponentInChildren<BaseGunController>();
    }

    public virtual void ShootingManager()
    {
        gunController.Shooting();
    }

    public virtual void ReloadManager()
    {
        gunController.Reload();
    }

    public virtual bool IsMagazineEmpty()
    {
        return gunController.IsMagazineEmpty();
    }

    public virtual bool OnReload()
    {
        return gunController.onReload;
    }

    public virtual bool OnCooldown()
    {
        return gunController.onCooldown;
    }

    public virtual bool OnAttack()
    {
        return gunController.onAttack;
    }

    public virtual float GetRotationSpeed()
    {
        return gunController.attack.rotationSpeed;
    }
}
