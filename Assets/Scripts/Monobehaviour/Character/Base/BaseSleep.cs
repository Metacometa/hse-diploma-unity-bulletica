using System.Collections;
using UnityEngine;

public class BaseSleep : MonoBehaviour
{
    public bool onSleep;
    private Level level;
    private BaseInvincibility invincibility;
    public BaseCharacter character;

    void Awake()
    {
        level = GetComponentInParent<Level>();
        onSleep = true;
        
        invincibility = GetComponentInParent<BaseInvincibility>();
        character = GetComponentInParent<BaseCharacter>();
    }

    public void Sleep()
    {
        onSleep = true;
        invincibility.invincible = true;
    }

    public void Wake()
    {
        StartCoroutine(AwakeningTimer());
    }

    private IEnumerator AwakeningTimer()
    {
        float awakeningDelay = Random.Range(level.gameParameters.awakeningDelay, level.gameParameters.maxAwakeningDelay);

        if (character)
        {
            character.OnAwake();
        }

        yield return new WaitForSeconds(awakeningDelay);

        if (invincibility)
        {
            invincibility.invincible = false;
        }

        onSleep = false;
    }

}
