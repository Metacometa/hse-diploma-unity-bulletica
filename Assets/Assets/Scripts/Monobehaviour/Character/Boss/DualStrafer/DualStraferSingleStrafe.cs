using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DualStraferSingleStrafe : DualStraferAbstractStrafe
{

    public override void StartStrafeHandler()
    {
        if (!OnStrafe)
        {
            GetPrelastPointDir();
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

    private void GetPrelastPointDir()
    {
        if (strafePoints.Count == 0) { return; }

        Transform player = GetComponentInParent<Level>().GetComponentInChildren<Player>().transform;

        SortedDictionary<float, int> distances = new SortedDictionary<float, int>();

        for(int i = 0; i < strafePoints.Count; ++i)
        {
            distances.Add(Vector3.Distance(transform.position, strafePoints[i]) + Vector3.Distance(player.transform.position, strafePoints[i]), i);
        }

        int index = distances.ElementAt(2).Value;

        strafeDir = (strafePoints[index] - (Vector2)transform.position).normalized;
    }

    private void OnDrawGizmos()
    {
/*        if (strafePoints.Count == 0) { return; }

        Transform player = GetComponentInParent<Level>().GetComponentInChildren<Player>().transform;

        SortedDictionary<float, int> distances = new SortedDictionary<float, int>();

        for (int i = 0; i < strafePoints.Count; ++i)
        {
            distances.Add(Vector3.Distance(transform.position, strafePoints[i]) + Vector3.Distance(player.transform.position, strafePoints[i]), i);
        }

        Gizmos.color = Color.red;
        Gizmos.DrawCube(strafePoints[distances.ElementAt(3).Value], new Vector3(2,2,2));


        Gizmos.color = Color.green;
        Gizmos.DrawCube(strafePoints[distances.ElementAt(2).Value], new Vector3(2, 2, 2));


        Gizmos.color = Color.blue;
        Gizmos.DrawCube(strafePoints[distances.ElementAt(0).Value], new Vector3(2, 2, 2));*/
    }
}
