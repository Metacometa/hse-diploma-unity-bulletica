using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

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
        audioSource.playOnAwake = false;
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

        BaseShooting shooting = GetComponent<BaseShooting>();

        if (shooting)
        {
            shooting.ReloadManager();
        }

        Light2D[] light = GetComponentsInChildren<Light2D>();

        foreach (Light2D light2d in light)
        {
            light2d.enabled = false;
        }

        Collider2D coll = GetComponent<Collider2D>();

        if (coll)
        {
            coll.isTrigger = true;
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
