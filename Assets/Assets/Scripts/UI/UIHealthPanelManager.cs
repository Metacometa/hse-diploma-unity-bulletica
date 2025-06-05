using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class UIHealthPanelManager : MonoBehaviour
{
    [SerializeField] private GameObject heartPrefab;
    private int currentHealth = 0;
    private List<UIHeart> hearts = new List<UIHeart>();
    private bool isProcessing = false;

    public BaseHealth playerHealth;
    private float animationDelay = 0.1f;
    private Queue<IEnumerator> actionQueue = new Queue<IEnumerator>();

    void Awake()
    {
        Player player = FindFirstObjectByType<Player>();

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

        currentHealth = heartsCount;
    }

    public void Heal(int heartsDelta)
    {
        if (heartsDelta <= 0) return;
        actionQueue.Enqueue(ProcessHeal(heartsDelta));
        TryProcessNext();
    }

    public void Damage(int heartsDelta)
    {
        if (heartsDelta <= 0) return;
        actionQueue.Enqueue(ProcessDamage(heartsDelta));
        TryProcessNext();
    }
    private void TryProcessNext()
    {
        if (!isProcessing && actionQueue.Count > 0)
        {
            StartCoroutine(ProcessAction(actionQueue.Dequeue()));
        }
    }
    private IEnumerator ProcessAction(IEnumerator action)
    {
        isProcessing = true;
        yield return StartCoroutine(action);
        isProcessing = false;
        TryProcessNext();
    }

    private IEnumerator ProcessHeal(int heartsDelta)
    {
        int maxHeal = hearts.Count - currentHealth;
        heartsDelta = Mathf.Min(heartsDelta, maxHeal);
        if (heartsDelta <= 0) yield break;

        int currentIndex = currentHealth;
        for (int i = 0; i < heartsDelta; i++)
        {
            int index = currentIndex + i;
            if (index < hearts.Count && hearts[index] != null)
            {
                hearts[index].Heal();
                yield return new WaitForSeconds(animationDelay);
            }
        }
        currentHealth += heartsDelta;
    }
    private IEnumerator ProcessDamage(int heartsDelta)
    {
        heartsDelta = Mathf.Min(heartsDelta, currentHealth);
        if (heartsDelta <= 0) yield break;

        int currentIndex = currentHealth - 1;
        for (int i = 0; i < heartsDelta; i++)
        {
            int index = currentIndex - i;
            if (index >= 0 && hearts[index] != null)
            {
                hearts[index].Damage();
                yield return new WaitForSeconds(animationDelay);
            }
        }
        currentHealth -= heartsDelta;
    }
}