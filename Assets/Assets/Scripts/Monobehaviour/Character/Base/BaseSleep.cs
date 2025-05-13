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
        onSleep = false;
        
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
        yield return new WaitForSeconds(level.gameParameters.awakeningDelay);

        if (invincibility)
        {
            invincibility.invincible = false;
        }

        onSleep = false;
    }

}
