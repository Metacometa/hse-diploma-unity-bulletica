using System.Collections;
using UnityEngine;

public class ReversedBullet : DelayedBullet
{
    public override void DelayedEffect() 
    {
        Reverse();
    }

    void Reverse()
    {
        rb.linearVelocity = -rb.linearVelocity;
    }
}
