using System.Collections;
using UnityEngine;

public class BaseInvincibility : MonoBehaviour
{
    public bool invincible;

    private BaseProfile profile;

    void Awake()
    {
        profile = GetComponent<BaseCharacter>().profile;
        invincible = false;
    }

    public void Invincible()
    {
        StartCoroutine(InvincibleTimer());
    }

    private IEnumerator InvincibleTimer()
    {
        invincible = true;

        yield return new WaitForSeconds(profile.invincibilityTime);

        invincible = false;
    }

}
