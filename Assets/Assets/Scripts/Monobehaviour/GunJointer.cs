using UnityEngine;

public class GunJointer : MonoBehaviour
{
    public FixedJoint2D joint;

    [SerializeField] private Vector2 pullingPoint;

    private Vector2 startingConnectedAnchor;

    void Awake()
    {
        joint = GetComponentInChildren<FixedJoint2D>();
        joint.connectedBody = transform.parent.GetComponent<Rigidbody2D>();

        startingConnectedAnchor = joint.connectedAnchor;
    }

    public void PullPosition(in float elapsedTime, in float duration)
    {
        joint.connectedAnchor = Vector3.Lerp(joint.connectedAnchor, pullingPoint, elapsedTime / duration);
        //transform.position = Vector3.Lerp(transform.position, pullingPoint, elapsedTime / duration);
    }

    public void RestorePosition(in float elapsedTime, in float duration)
    {
        joint.connectedAnchor = Vector3.Lerp(joint.connectedAnchor, startingConnectedAnchor, elapsedTime / duration);
        //transform.position = Vector3.Lerp(transform.position, Vector3.zero, elapsedTime / duration);
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pullingPoint, 0.15f);
    }
}
