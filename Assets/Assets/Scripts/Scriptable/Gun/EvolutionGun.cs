using UnityEngine;

[CreateAssetMenu(fileName = "EvolutionGun", menuName = "Scriptable Objects/Guns/Evolution")]
public class EvolutionGun : BurstGun
{
    public override void LoadGun(ref float bulletsInMagazine, ref float magazineCapacity)
    {
        bulletsInMagazine = ++magazineCapacity;
    }

    public override int getMagazineCapacity()
    {
        return magazineCapacity;
    }
}
