using UnityEngine;

public class BaseTargeting : MonoBehaviour
{
    public Transform target;

    public bool targetSeen;

    public void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag(GetComponent<BaseCharacter>().profile.targetTag)?.transform;
    }
}
