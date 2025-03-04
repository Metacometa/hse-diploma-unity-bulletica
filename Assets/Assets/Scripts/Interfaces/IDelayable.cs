using System.Collections;
using UnityEngine;

public interface IDelayable
{
    public IEnumerator DelayTimer();
    public void DelayedEffect();
}
