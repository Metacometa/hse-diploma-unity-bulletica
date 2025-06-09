using UnityEngine;

public class HealthPointItem : MonoBehaviour
{
    private Transform target;
    private PlayerHealth playerHealth;

    [SerializeField] private string playerTag;

    private Vector3 startPoint;

    public bool playerInjured = false;

    [SerializeField] private float speed;
    [SerializeField] private float speedToPlayer = 4f;
    [SerializeField] private float distanceToReact;

    private Rigidbody2D rb;

    void Awake()
    {
        target = GameObject.FindGameObjectWithTag(playerTag)?.transform;

        playerHealth = target.GetComponent<PlayerHealth>();

        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        startPoint = transform.position;
    }

    private void Update()
    {
        UpdatePlayerInjured();

        float distance = Vector3.Distance(target.position, transform.position);

        if (playerInjured && distance <= distanceToReact)
        {
            rb.linearVelocity = CalculateDir(transform.position, target.position) * speedToPlayer;
            //transform.localPosition = Vector3.Lerp(transform.localPosition, target.position, Time.deltaTime * speed);
        }
        else
        {
            float distanceToStartPoint = Vector3.Distance(transform.position, startPoint);

            if (distanceToStartPoint > 0.2f)
            {
                rb.linearVelocity = CalculateDir(transform.position, startPoint) * speed;
            }
            else
            {
                rb.linearVelocity = Vector3.zero;
            }
            //transform.localPosition = Vector3.Lerp(transform.localPosition, startPoint, Time.deltaTime * speed);
        }
    }

    Vector3 CalculateDir(Vector3 from, Vector3 to)
    {
        return (to - from).normalized;
    }

    void UpdatePlayerInjured()
    {
        if (playerHealth)
        {
            playerInjured = playerHealth.PlayerInjured();
        }
        else
        {
            playerInjured = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag(playerTag) 
            && playerHealth.PlayerInjured())
        {
            gameObject.SetActive(false);
        }
    }
}
