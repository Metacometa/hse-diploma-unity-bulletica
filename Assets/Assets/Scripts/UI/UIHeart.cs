using UnityEngine;

public class UIHeart : MonoBehaviour
{
    private Animator animator;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        animator.SetTrigger("Damage");
    }

    public void Heal()
    {
        animator.SetTrigger("Heal");
    }
}
