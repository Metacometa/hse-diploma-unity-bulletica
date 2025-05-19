using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class BaseMovement : MonoBehaviour, IMoveable, IPushable
{
    private BaseRotator rotator;

    /*[HideInInspector]*/
    public bool onPush;
    protected BaseProfile profile;

    public float timeCounter;

    protected Rigidbody2D rb;

    protected virtual void Awake()
    {
        onPush = false;

        profile = GetComponent<BaseCharacter>().profile;
        rotator = GetComponent<BaseRotator>();

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
        //rb.AddForce(dir.normalized * profile.pushingAwayForce, ForceMode2D.Impulse);

        StartCoroutine(PushAwayManager());
    }

    public void PushAway()
    {
        StartCoroutine(PushAwayManager());
    }

    public void Push()
    {
        Vector2 dir = -rotator.tankDir;

        rb.linearVelocity = dir * profile.pushingAwayForce;
        Buffering();
        //rb.AddForce(dir.normalized * profile.pushingAwayForce, ForceMode2D.Impulse);
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
