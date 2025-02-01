using UnityEngine;

public interface IPushable
{
    public void PushAway(ref Rigidbody2D rb, in Vector2 dir);
}
