using System.Collections.Generic;
using UnityEngine;

public abstract class DualStraferAbstractStrafe : MonoBehaviour
{
    public Vector2 strafeDir;

    public bool OnStrafe;
    public bool OnCooldown;

    public DualStraferShooting shooting;

    public List<Vector2> strafePoints;

    protected DualStraferProfile profile;

    void Awake()
    {
        OnStrafe = false;
        shooting = GetComponent<DualStraferShooting>();

        profile = (DualStraferProfile)GetComponent<BaseCharacter>().profile;

        Transform chamber = GetComponentInParent<Chamber>().transform;
        foreach (StrafePoint point in chamber.GetComponentsInChildren<StrafePoint>())
        {
            strafePoints.Add(point.transform.position);
        }
    }

    public abstract void StartStrafeHandler();

    public abstract void EndStrafeHandler();

    protected void Strafe()
    {
        shooting.Refresh();

        OnStrafe = true;
    }
}
