using System.Collections.Generic;
using UnityEngine;

public class UIHealthPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    private int currentHeal = 0;

    private List<UIHeart> hearts = new List<UIHeart>();

    private BaseHealth playerHealth;

    void Awake()
    {
        Player player = transform.parent.GetComponentInChildren<Player>();

        if (player)
        {
            playerHealth = player.GetComponent<BaseHealth>();
        }
    }

    void Start()
    {
        if (playerHealth)
        {
            Initialize(playerHealth.health);
        }   
    }

    public void Initialize(int heartsCount)
    {
        for (int i = 0; i < heartsCount; i++)
        {
            UIHeart heart = Instantiate(heartPrefab, transform).GetComponent<UIHeart>();
            hearts.Add(heart);
        }

        currentHeal = heartsCount;
    }

    public void Heal(int heartsDelta)
    {
        int curIndex = currentHeal - 1;

        heartsDelta = curIndex + 1 + heartsDelta > hearts.Count ? hearts.Count - (curIndex + 1) : heartsDelta;

        for (int i = 0; i < heartsDelta; i++)
        {
            hearts[curIndex+i].Heal();
        }

        currentHeal += heartsDelta;
    }
    public void Damage(int heartsDelta)
    {
        int curIndex = currentHeal - 1;

        heartsDelta = curIndex + 1 - heartsDelta >= 0 ? heartsDelta : curIndex + 1;

        for (int i = 0; i < heartsDelta; i++)
        {
            hearts[curIndex-i].Damage();
        }

        currentHeal -= heartsDelta;
    }
}