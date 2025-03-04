using UnityEngine;

public class GunAnimator : MonoBehaviour
{
    [SerializeField] private Vector2 pullingPoint;

    private bool onPulling;

    private void Start()
    {
        onPulling = false;        
    }

    public void PullPosition(in float elapsedTime, in float duration)
    {
        transform.position = Vector3.Lerp(transform.position, pullingPoint, elapsedTime / duration);
    }

    public void RestorePosition(in float elapsedTime, in float duration)
    {
        transform.position = Vector3.Lerp(transform.position, Vector3.zero, elapsedTime / duration);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(pullingPoint, 0.15f);
    }
}
