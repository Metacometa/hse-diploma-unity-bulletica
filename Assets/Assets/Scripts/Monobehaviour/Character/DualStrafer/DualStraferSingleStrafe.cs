using System.Collections;
using UnityEngine;

public class DualStraferSingleStrafe : DualStraferAbstractStrafe
{

    public override void StartStrafeHandler()
    {
        if (!OnStrafe)
        {
            GetStrafeDirRandomly();
            Strafe();
        }
    }

    public override void EndStrafeHandler()
    {
        StartCoroutine(Cooldown());
    }

    private IEnumerator Cooldown()
    {
        OnStrafe = false;
        shooting.Drain();

        OnCooldown = true;
        yield return new WaitForSeconds(profile.strafeCooldown);
        OnCooldown = false;
    }

    private void GetStrafeDirRandomly()
    {
        if (strafePoints.Count == 0) { return; }

        int point_i = Random.Range(0, strafePoints.Count);

        Debug.Log(point_i);

        strafeDir = (strafePoints[point_i] - (Vector2)transform.position).normalized;
    }
}
