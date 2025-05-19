using UnityEngine;

public class BaseTargeting : MonoBehaviour
{
    private Transform target;

    public bool inSight;
    public bool inPursueRange;
    public bool inShootingRange;

    private BaseCharacter character;

    public void SetTarget()
    {
        character = GetComponent<BaseCharacter>();
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

    public Vector3 PredictedPosition()
    {
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();

        if (!rb) { return position(); }

        Vector3 prediction = rb.linearVelocity;

        return position() + prediction * character.profile.predictionDepth;
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
