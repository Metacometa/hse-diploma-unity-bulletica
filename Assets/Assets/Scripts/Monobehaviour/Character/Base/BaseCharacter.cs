using UnityEngine;

[RequireComponent (typeof(BaseHealth))]
[RequireComponent(typeof(BaseMovement))]
[RequireComponent(typeof(BaseDeath))]
[RequireComponent(typeof(BaseShimmer))]
public class BaseCharacter : MonoBehaviour
{
    protected BaseHealth health;
    protected BaseMovement move;
    protected BaseDeath death;
    protected BaseShimmer shimmer;

    protected Rigidbody2D rb;

    public bool onSleep;

    protected virtual void Awake()
    {
        health = GetComponent<BaseHealth>();
        move = GetComponent<BaseMovement>();
        death = GetComponent<BaseDeath>();
        shimmer = GetComponent<BaseShimmer>();

        rb = GetComponent<Rigidbody2D>();

        health.healthPoints = health.startingHealth;
    }
}
