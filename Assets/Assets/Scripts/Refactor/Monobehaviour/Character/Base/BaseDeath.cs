using UnityEngine;

public class BaseDeath : MonoBehaviour, IDeathable
{
    public void Die(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
