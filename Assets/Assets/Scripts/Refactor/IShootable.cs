using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface IShootable
{
    public bool IsMagazineEmpty();

    public void RotateGun(in Vector2 to);

    public void ShootingManager(in Vector2 to);

    public void ReloadManager();
}
