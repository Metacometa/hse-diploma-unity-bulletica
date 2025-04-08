using UnityEngine;

public class DualStraferShooting : BossShooting
{
    private Attacks attack;
    enum Attacks { DoubleAttack, AltertaneAttack};

    protected override void Awake()
    {
        gunController = GetComponentsInChildren<BaseGunController>();
        currentGun = gunController[0];

        attack = Attacks.DoubleAttack;
        Switch = false;
    }

    public void AltertaneAttack()
    {
        SetAttack(1);

        if (Switch)
        {
            currentGun = gunController[0];
        }
        else
        {
            currentGun = gunController[1];
        }

        currentGun.Shooting();
        Switch = !Switch;
        attack = Attacks.AltertaneAttack;
    }

    public void DoubleAttack()
    {
        SetAttack(0);

        gunController[0].Shooting();
        gunController[1].Shooting();
        attack = Attacks.DoubleAttack;
    }

    public override void ShootingManager()
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

    public override void ReloadManager()
    {
        switch(attack)
        {
            case Attacks.AltertaneAttack:
                currentGun.Reload();
                break;
            case Attacks.DoubleAttack:
                gunController[0].Reload();
                gunController[1].Reload();
                break;
        }

    }

    public void Refresh()
    {
        foreach (BaseGunController gc in gunController)
        {
            gc.Refresh();
        }
    }

    public void Drain()
    {
        foreach (BaseGunController gc in gunController)
        {
            gc.Drain();
        }
    }

    public void SetAttack(in int i)
    {
        foreach (BaseGunController gc in gunController)
        {
            gc.SetAttack(i);
        }
    }
}
