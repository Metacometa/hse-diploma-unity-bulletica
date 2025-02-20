using UnityEngine;

public class BaseTargeting : MonoBehaviour
{
    public Transform target;
    public string targetTag;

    public bool targetSeen;

    public void SetTarget()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
}
