using UnityEngine;

public class AccumulatingBullet : BaseBullet
{
    [SerializeField] private GameObject player;
    private bool velocityChanged;

    void Start()
    {
        velocityChanged = false;
    }

    void Update()
    {
/*        if (!velocityChanged && (transform.position.x == player.transform.position.x
            || transform.position.y == player.transform.position.y))
        {
            ChangeVelocity();
        }*/

        ChangeVelocity();
    }

    private void ChangeVelocity()
    {
        Vector2 dir = player.transform.position - transform.position;
        rb.linearVelocity = speed * dir;

        velocityChanged = true;
    }
}
