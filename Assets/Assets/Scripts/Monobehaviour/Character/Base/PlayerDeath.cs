using UnityEngine;

public class PlayerDeath : BaseDeath
{
    public override void Die(GameObject gameObject)
    {

        //Destroy(gameObject);
        gameObject.SetActive(false);
    }
}
