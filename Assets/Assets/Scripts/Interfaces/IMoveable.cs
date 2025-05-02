using UnityEngine;

public interface IMoveable
{
    public void Move(in Vector2 dir);
    public void Stop();
}
