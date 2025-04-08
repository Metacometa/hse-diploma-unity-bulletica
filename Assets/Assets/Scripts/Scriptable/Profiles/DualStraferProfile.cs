using UnityEngine;

[CreateAssetMenu(fileName = "DualStrafer", menuName = "Scriptable Objects/Profiles/Bosses/DualStrafer")]
public class DualStraferProfile : BossProfile
{
    [Header("Strafe")]
    [SerializeField] public float strafeSpeed;
    [SerializeField] public float strafeCooldown;

    [Header("RageStrafe")]
    [SerializeField] public float rageStrafeCooldown;

    [Space]
    [SerializeField] public int endlessStrafeHealthTrigger;
}
