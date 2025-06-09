using UnityEngine;

public class Item : MonoBehaviour
{
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        RotateSprite();
    }

    void RotateSprite()
    {
        transform.Rotate(new Vector3(0, rotationSpeed * Time.deltaTime, 0));
    }
}
