using System.Collections;
using UnityEngine;
using static Unity.VisualScripting.Member;

public interface IShootable
{
    public bool IsMagazineEmpty();
    public void ShootingManager();
    public void ReloadManager();
}
