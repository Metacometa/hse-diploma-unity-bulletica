using System;
using UnityEngine;

public class TurretShooting : BaseShooting
{
    enum Attacks { ShotgunAttack, BaseAttack };

    public override void Awake()
    {
        base.Awake();

        SetBaseAttack();
    }

    public void SetShotgunAttack()
    {
        gunController.SetAttack((int)Attacks.ShotgunAttack);
    }

    public void SetBaseAttack()
    {
        gunController.SetAttack((int)Attacks.BaseAttack);
    }

    public void Refresh()
    {
        gunController.Refresh();
    }

    public void Drain()
    {
        gunController.Drain();
    }
}
