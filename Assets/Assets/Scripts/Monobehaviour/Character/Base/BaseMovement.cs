using System.Collections;
using UnityEngine;
using UnityEngine.Windows;

public class BaseMovement : MonoBehaviour, IMoveable, IPushable
{
    /*[HideInInspector]*/ public bool onPush;
    private BaseProfile profile;

    protected virtual void Awake()
    {
        onPush = false;

        profile = GetComponent<BaseCharacter>().profile;
    }
    protected virtual void Start()
    {
        onPush = false;
    }

    public void Move(ref Rigidbody2D rb, in Vector2 dir, in float speed)
    {
        rb.linearVelocity = dir.normalized * speed;
    }

    public void StopMovement(ref Rigidbody2D rb)
    {
        rb.linearVelocity = Vector2.zero;
    }

    public void PushAway(ref Rigidbody2D rb, in Vector2 dir)
    {
        rb.linearVelocity = Vector2.zero;
        rb.AddForce(dir.normalized * profile.pushingAwayForce, ForceMode2D.Impulse);

        StartCoroutine(PushAwayManager());
    }

    IEnumerator PushAwayManager()
    {
        onPush = true;

        yield return new WaitForSeconds(profile.pushingAwayTime);

        onPush = false;
    }
}
