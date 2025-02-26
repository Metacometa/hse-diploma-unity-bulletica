using UnityEngine;

public class BossAgarController : MonoBehaviour
{
    [SerializeField] private GameObject agar;
    [SerializeField] public int maxAgar;

    [SerializeField] public int bouncesToChase;

    [SerializeField] private float scaleProportion;

    void Awake()
    {
        GameObject newAgar = Instantiate(agar, transform);

        newAgar.transform.position = transform.position;
        newAgar.transform.position = transform.position;
    }

    public void SpawnMiniAgars(in Transform agarTransform, int agarCounter, in Rigidbody2D rb)
    {
        GameObject newAgar1 = Instantiate(agar, transform);
        GameObject newAgar2 = Instantiate(agar, transform);

        newAgar1.transform.position = agarTransform.position;
        newAgar2.transform.position = agarTransform.position;

        newAgar1.transform.localScale = agarTransform.localScale * scaleProportion;
        newAgar2.transform.localScale = agarTransform.localScale * scaleProportion;

        newAgar1.GetComponent<BossAgar>().agarCounter = agarCounter + 1;
        newAgar2.GetComponent<BossAgar>().agarCounter = agarCounter + 1;
/*
        newAgar1.GetComponent<BossAgar>().startDir = rb.linearVelocity.normalized;
        newAgar2.GetComponent<BossAgar>().startDir = rb.linearVelocity.normalized;*/
    }
}
