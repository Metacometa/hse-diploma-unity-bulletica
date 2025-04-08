using UnityEngine;

public class StrafePoint : MonoBehaviour
{
    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DualStrafer strafer = collision.GetComponent<DualStrafer>();

        if (strafer != null)
        {
            strafer.currentStrafe.EndStrafeHandler();
        }
    }
}
