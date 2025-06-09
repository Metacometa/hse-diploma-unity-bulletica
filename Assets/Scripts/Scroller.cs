using UnityEngine;
using UnityEngine.UI;

public class Scroller : MonoBehaviour
{
    [SerializeField] private Renderer bgRenderer;
    [SerializeField] private float speed;

    public Rigidbody2D input;

    void Awake()
    {
        input = transform.parent.transform.GetComponentInChildren<Player>().transform.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        bgRenderer.material.mainTextureOffset += input.linearVelocity.normalized * speed;
        transform.localRotation = Quaternion.AngleAxis(0, Vector3.forward);
        transform.rotation = Quaternion.AngleAxis(0, Vector3.forward);
    }
}
