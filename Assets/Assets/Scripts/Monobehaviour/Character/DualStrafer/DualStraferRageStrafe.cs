using System.Collections;
using UnityEngine;

public class DualStraferRageStrafe : DualStraferAbstractStrafe
{
    //endlessStrafe
    private int nextPoint;

    public override void StartStrafeHandler()
    {
        if (!OnStrafe)
        {
            GetStrafeDir(nextPoint);
            Strafe();
        }
    }

    public override void EndStrafeHandler()
    {
        nextPoint = ++nextPoint % 4;
        StartCoroutine(Cooldown());
    }


    private IEnumerator Cooldown()
    {
        OnStrafe = false;
        shooting.Drain();

        OnCooldown = true;
        yield return new WaitForSeconds(profile.rageStrafeCooldown);
        OnCooldown = false;
    }

    private void GetStrafeDir(in int index)
    {
        if (index >= strafePoints.Count) { return; }

        strafeDir = (strafePoints[index] - (Vector2)transform.position).normalized;
    }
}
