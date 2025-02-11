using System.Collections;
using UnityEngine;

public class DelayedBullet : BaseBullet, IDelayable
{
    [SerializeField] protected float delayTimer;

    public virtual void Start()
    {
        StartCoroutine(DelayTimer());
    }

    public IEnumerator DelayTimer() 
    {
        yield return new WaitForSeconds(delayTimer);
        DelayedEffect();
    }

    public virtual void DelayedEffect() {}
}
