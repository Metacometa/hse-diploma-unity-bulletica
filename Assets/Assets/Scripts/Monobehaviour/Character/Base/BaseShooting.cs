using UnityEngine;

public class BaseShooting : MonoBehaviour, IShootable
{
    public BaseGunController gunController;

    void Awake()
    {
        gunController = GetComponentInChildren<BaseGunController>();
    }

    public void ShootingManager()
    {
        gunController.Shooting();
    }

    public void ReloadManager()
    {
        gunController.Reload();
    }

    public bool IsMagazineEmpty()
    {
        return gunController.IsMagazineEmpty();
    }

    public bool OnReload()
    {
        return gunController.onReload;
    }

    public bool OnCooldown()
    {
        return gunController.onCooldown;
    }

    public bool OnAttack()
    {
        return gunController.onAttack;
    }

    public float GetRotationSpeed()
    {
        return gunController.attack.rotationSpeed;
    }
}
