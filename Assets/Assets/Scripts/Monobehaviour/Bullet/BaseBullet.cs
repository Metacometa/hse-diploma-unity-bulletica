using UnityEngine;

public class BaseBullet : MonoBehaviour
{
    protected Rigidbody2D rb;

    public float speed;
    public float force;

    public bool enemy = true;

    [SerializeField] private AudioSource audioSource;
    private MusicManager musicManager;

    [SerializeField] public string category;
    [SerializeField] public int maxNumber;

    public BulletPoolingManager poolingManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        Level level = FindFirstObjectByType<Level>();
        if (level)
        {
            musicManager = level.GetComponent<MusicManager>();
            poolingManager = level.GetComponentInChildren<BulletPoolingManager>();
        }
    }

    public void ShapeBullet(in Vector2 dir, in float angle)
    {
        rb = GetComponent<Rigidbody2D>();   
        rb.linearVelocity = Quaternion.AngleAxis(angle, Vector3.forward) * dir * speed;

        if (audioSource && musicManager)
        {
            audioSource.volume = musicManager.soundParameters.volume;
            audioSource.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.transform.CompareTag("Obstacle") 
            || col.transform.CompareTag("Door"))
        {
            gameObject.SetActive(false);
            //Destroy(gameObject);
        }

        if (enemy)
        {
            if (col.transform.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
        else
        {
            if (!col.transform.CompareTag("Player"))
            {
                gameObject.SetActive(false);
                //Destroy(gameObject);
            }
        }
    }
}
