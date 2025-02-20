using UnityEngine;

public class GunJointer : MonoBehaviour
{
    public FixedJoint2D joint;
    void Awake()
    {
        joint = GetComponentInChildren<FixedJoint2D>();
        joint.connectedBody = transform.parent.GetComponent<Rigidbody2D>();
    }
}
