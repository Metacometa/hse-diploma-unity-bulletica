using UnityEngine;

public class BaseTargeting : MonoBehaviour
{
    private Transform target;

    public bool inSight;
    public bool inPursueRange;
    public bool inShootingRange;

    public void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag(GetComponent<BaseCharacter>().profile.targetTag)?.transform;
    }

    public Vector3 position()
    {
        if (target && target.gameObject.activeInHierarchy)
        {
            return target.position;
        }

        return Vector3.zero;
    }

    public string Tag()
    {
        if (target)
        {
            return target.tag;
        }

        return "";
    }
}
