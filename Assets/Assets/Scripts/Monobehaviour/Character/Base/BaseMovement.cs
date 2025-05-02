using System.Collections;
using UnityEngine;

public class BaseMovement : MonoBehaviour, IMoveable, IPushable
{
    /*[HideInInspector]*/ public bool onPush;
    protected BaseProfile profile;

    public float timeCounter;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        onPush = false;

        profile = GetComponent<BaseCharacter>().profile;

        rb = GetComponent<Rigidbody2D>();
    }
    protected virtual void Start()
    {
        onPush = false;
    }

    void Update()
    {
        timeCounter -= Time.deltaTime;
        timeCounter = Mathf.Clamp(timeCounter, 0f, profile.stayBufferTime);
    }

    public virtual void Move(in Vector2 dir)
    {
        Move(dir, profile.moveSpeed);
    }

    public virtual void Move(in Vector2 dir, float speed)
    {
        rb.linearVelocity = dir.normalized * speed;
    }

    public virtual void Stop()
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

    public void Buffering()
    {
        timeCounter = profile.stayBufferTime;
    }

    public bool CanMove()
    {
        return timeCounter <= 0f;
    }
}
