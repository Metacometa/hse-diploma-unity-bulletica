using UnityEngine;
using UnityEngine.UI;

public class UIHeart : MonoBehaviour
{
    private Animator animator;
    private Image heartImage;
    [SerializeField] private Sprite healthySprite;
    [SerializeField] private Sprite damagedSprite;

    public void SetHealthy() => heartImage.sprite = healthySprite;
    public void SetDamaged() => heartImage.sprite = damagedSprite;

    private void Awake()
    {
        heartImage = GetComponent<Image>();
        //animator = GetComponent<Animator>();
    }

    public void Damage()
    {
        SetDamaged();
        //animator.SetTrigger("Damage");
    }

    public void Heal()
    {
        SetHealthy();
        //animator.SetTrigger("Heal");
    }
}
