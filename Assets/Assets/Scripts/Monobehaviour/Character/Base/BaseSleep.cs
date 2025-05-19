using System.Collections;
using UnityEngine;

public class BaseSleep : MonoBehaviour
{
    public bool onSleep;
    private Level level;
    private BaseInvincibility invincibility;

    void Awake()
    {
        level = GetComponentInParent<Level>();
        onSleep = true;
        
        invincibility = GetComponentInParent<BaseInvincibility>();
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
        yield return new WaitForSeconds(awakeningDelay);

        if (invincibility)
        {
            invincibility.invincible = false;
        }

        onSleep = false;
    }

}
