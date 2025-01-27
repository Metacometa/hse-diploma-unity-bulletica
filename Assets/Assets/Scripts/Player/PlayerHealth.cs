using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    private Player player;

    private SpriteRenderer sprite;
    private Color startColor;

    [SerializeField] private float loosingControlTime;
    [SerializeField] private int shimmerTimes;

    [SerializeField] private float invincibilityTime;

    [HideInInspector] public bool isLostControl;
    [HideInInspector] public bool isInvincible;
    
    [SerializeField] private int startingHealths;
    [HideInInspector] public int healths;

    void Start()
    {
        player = GetComponent<Player>();

        sprite = GetComponent<SpriteRenderer>();

        startColor = sprite.color;

        healths = startingHealths;
    }

    public void TakeDamage()
    {
        healths = Mathf.Max(0, healths - 1);

        StartCoroutine(LoosingControl());
        StartCoroutine(InvincibleTime());
    }

    public IEnumerator InvincibleTime()
    {
        isInvincible = true;

        for (int i = 0; i < shimmerTimes; ++i)
        {
            ChangeColor();
            yield return new WaitForSeconds(invincibilityTime / shimmerTimes / 2);
            RestoreColor();
            yield return new WaitForSeconds(invincibilityTime / shimmerTimes / 2);
        }

        isInvincible = false;
    }

    IEnumerator LoosingControl()
    {
        isLostControl = true;

        yield return new WaitForSeconds(loosingControlTime);

        isLostControl = false;
        player.StopMoving();
    }

    void ChangeColor()
    {
        sprite.color = new Color(1, 0, 0, 1);
    }

    void RestoreColor()
    {
        sprite.color = startColor;
    }

}
