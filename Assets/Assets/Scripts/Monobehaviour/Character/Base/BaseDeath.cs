using System.Collections;
using UnityEngine;

public class BaseDeath : MonoBehaviour, IDeathable
{
    protected AudioSource audioSource;
    protected MusicManager musicManager;
    
    public bool died = false;

    void Awake()
    {
        musicManager = GetComponentInParent<MusicManager>();

        audioSource = gameObject.AddComponent<AudioSource>();
        audioSource.clip = musicManager.soundParameters.enemyDeath;
    }

    public virtual void Die(GameObject gameObject)
    {
        died = true;
        MakeDeathSound();

        SpriteRenderer[] sprites = GetComponentsInChildren<SpriteRenderer>();

        foreach (SpriteRenderer sprite in sprites)
        {
            sprite.enabled = false;
        }

        StartCoroutine(DieCoroutine());
    }

    public virtual void MakeDeathSound()
    {
        if (audioSource && musicManager)
        {
            audioSource.volume = musicManager.soundParameters.volume;

            audioSource.Play();
        }
    }

    IEnumerator DieCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
