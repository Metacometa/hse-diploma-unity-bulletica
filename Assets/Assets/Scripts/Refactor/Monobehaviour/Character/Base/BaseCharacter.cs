using UnityEngine;

public class BaseCharacter : MonoBehaviour
{
    protected BaseHealth health;
    protected BaseMovement move;
    protected BaseDeath death;
    protected BaseShimmer shimmer;

    protected Rigidbody2D rb;

    protected virtual void Start()
    {
        health = GetComponent<BaseHealth>();
        move = GetComponent<BaseMovement>();
        death = GetComponent<BaseDeath>();
        shimmer = GetComponent<BaseShimmer>();

        rb = GetComponent<Rigidbody2D>();
    }
}
