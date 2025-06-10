using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    private Transform point;

    [SerializeField] private Vector2 pullingPoint;

    private Vector2 startPosition;

    public bool reversedDirections;

    public bool onPulling;
    public float elapsedTime;
    public float duration;

    void Awake()
    {
        point = transform.Find("Point");
        onPulling = true;
        elapsedTime = 1;
        duration = 0;
    }
    private void Update()
    {
        if (elapsedTime > duration)
        {
            return;
        }

        elapsedTime += Time.deltaTime;

        float speed = elapsedTime / duration;
        if (duration == 0)
        {
            speed = 1;
        }


        if (onPulling ^ reversedDirections)
        {
            point.transform.localPosition = Vector3.Lerp(startPosition, pullingPoint, speed);
        }
        else
        {
            point.transform.localPosition = Vector3.Lerp(startPosition, Vector3.zero, speed);
        }
    }

    public void PullPosition(in float duration)
    {
        //joint.connectedAnchor = Vector3.Lerp(joint.connectedAnchor, pullingPoint, elapsedTime / duration);
        startPosition = point.transform.localPosition;
        onPulling = true;
        elapsedTime = 0;
        this.duration = duration;
    }

    public void RestorePosition(in float duration)
    {
        //joint.connectedAnchor = Vector3.Lerp(joint.connectedAnchor, startingConnectedAnchor, elapsedTime / duration);
        startPosition = point.transform.localPosition;
        onPulling = false;
        elapsedTime = 0;
        this.duration = duration;
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pullingPoint, 0.15f);
    }
}
