using System.Collections;
using UnityEngine;

public class BaseMovement : MonoBehaviour, IMoveable, IPushable
{
    /*[HideInInspector]*/ public bool onPush;
    protected BaseProfile profile;

    public float timeCounter;

    protected virtual void Awake()
    {
        onPush = false;

        profile = GetComponent<BaseCharacter>().profile;
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

    public virtual void Move(ref Rigidbody2D rb, in Vector2 dir, in float speed)
    {
        rb.linearVelocity = dir.normalized * speed;
    }

    public virtual void StopMovement(ref Rigidbody2D rb)
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
