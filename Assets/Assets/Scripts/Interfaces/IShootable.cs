using System.Collections;
using UnityEngine;

public interface IShootable
{
    public bool IsMagazineEmpty();
    public void ShootingManager();
    public void ReloadManager();
}
