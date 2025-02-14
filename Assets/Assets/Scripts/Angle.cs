using UnityEngine;

public class Angle : MonoBehaviour
{
    public Transform player;
    public Transform enemy;

    void Update()
    { 
        if (player != null && enemy != null)
        {
            Vector2 dir = player.position - enemy.position;
/*            //Debug.Log(Quaternion.LookRotation(dir));
            Debug.Log(Vector2.Angle(player.position, enemy.position));*/
        }

    }
}
