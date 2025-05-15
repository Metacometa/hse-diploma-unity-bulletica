using UnityEngine;

[RequireComponent(typeof(BaseHealth))]
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(BaseDeath))]

[RequireComponent(typeof(BaseShimmer))]
[RequireComponent(typeof(BaseRotator))]

[RequireComponent(typeof(BaseInvincibility))]
[RequireComponent(typeof(BaseSleep))]

public class BaseCharacter : MonoBehaviour
{
    protected BaseHealth health;
    protected BaseMovement move;
    protected BaseDeath death;

    protected BaseShimmer shimmer;
    protected BaseRotator rotator;

    protected BaseInvincibility invincibility;
    public BaseSleep sleep;

    protected Rigidbody2D rb;

    public BaseProfile profile;

    protected virtual void Awake()
    {
        health = GetComponent<BaseHealth>();
        move = GetComponent<BaseMovement>();
        death = GetComponent<BaseDeath>();
        shimmer = GetComponent<BaseShimmer>();
        rotator = GetComponent<BaseRotator>();
        sleep = GetComponent<BaseSleep>();
        invincibility = GetComponent<BaseInvincibility>();

        rb = GetComponent<Rigidbody2D>();
    }
}
