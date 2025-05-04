using UnityEngine;

public class BaseDeath : MonoBehaviour, IDeathable
{
    public virtual void Die(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
