using UnityEngine;

public class BaseTargeting : MonoBehaviour
{
    public Transform target;
    public string targetTag;

    public bool targetSeen;

    void Start()
    {
        target = GameObject.FindGameObjectWithTag(targetTag).transform;
    }
}
