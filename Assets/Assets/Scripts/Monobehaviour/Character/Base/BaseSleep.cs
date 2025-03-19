using System.Collections;
using UnityEngine;

public class BaseSleep : MonoBehaviour
{
    public bool onSleep;
    private Level level;

    void Awake()
    {
        level = GetComponentInParent<Level>();
        onSleep = false;
    }

    public void Sleep()
    {
        onSleep = true;
    }

    public void Wake()
    {
        StartCoroutine(AwakeningTimer());
    }

    private IEnumerator AwakeningTimer()
    {
        yield return new WaitForSeconds(level.gameParameters.awakeningDelay);

        onSleep = false;
    }

}
