using UnityEngine;

public class BulletPooler : MonoBehaviour
{
    public int pointer = 0;
    public int maxBullets = 0;

    public GameObject EnableBullet(Vector3 position)
    {
        if (pointer >= transform.childCount) { return null; }

        Transform childBullet = transform.GetChild(pointer);
        childBullet.localPosition = position;
        childBullet.gameObject.SetActive(true);
        
        pointer++;
        pointer %= maxBullets;

        return childBullet.gameObject;
    }
}

