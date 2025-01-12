using UnityEngine;

public class EnemyData : MonoBehaviour
{
/*    [HideInInspector] public bool moving;
    [HideInInspector] public bool attacking;
    [HideInInspector] public bool targetSeen;
    [HideInInspector] public bool targetInAttackRange;*/

    public bool moving;
    public bool attacking;
    public bool targetSeen;
    public bool targetInAttackRange;

    [SerializeField] public LayerMask targetMask;

    /*    void UpdateAnimationsVariables()
        {
            if (rb.linearVelocityX != 0 || rb.linearVelocityY != 0)
            {
                anim.SetBool("moved", true);
            }
            else
            {
                anim.SetBool("moved", false);
            }
        }*/

}
