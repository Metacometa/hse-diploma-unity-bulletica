using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface IShootable
{
    public bool IsMagazineEmpty();

    public void RotateGun(in Vector2 to);
    public void RotateGunInstantly(in Vector2 dir);

    public void ShootingManager();
    public void ReloadManager();
}
