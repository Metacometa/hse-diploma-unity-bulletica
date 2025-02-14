using UnityEngine;

public interface IObservable
{
    public void Observe();

    public void LookToPoint(in Vector2 dir, in float length, in LayerMask masks, ref bool boolFlag);

}
