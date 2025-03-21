using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class BaseMovement : MonoBehaviour, IMoveable, IPushable
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float pushingAwayTime;
    [SerializeField] private float pushingAwayForce;

    /*[HideInInspector]*/ public bool onPush;

    protected virtual void Awake()
    {
        onPush = false;
    }
    protected virtual void Start()
    {
        onPush = false;
    }

    public void Move(ref Rigidbody2D rb, in Vector2 dir)
    {
        rb.linearVelocity = dir.normalized * moveSpeed;
    }

    public void StopMovement(ref Rigidbody2D rb)
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void PushAway(ref Rigidbody2D rb, in Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir.normalized * pushingAwayForce, ForceMode2D.Impulse);

        StartCoroutine(PushAwayManager());
    }

    IEnumerator PushAwayManager()
    {
        onPush = true;

        yield return new WaitForSeconds(pushingAwayTime);

        onPush = false;
    }
}
