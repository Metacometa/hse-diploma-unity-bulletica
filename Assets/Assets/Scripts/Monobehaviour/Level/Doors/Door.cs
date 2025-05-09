using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private float openingSpeed;
    [SerializeField] private float closingSpeed;

    private Vector3 leftOpenedPos;
    private Vector3 rightOpenedPos;

    private Rigidbody2D leftDoor;
    private Rigidbody2D rightDoor;

    private Vector3 leftClosedPos;
    private Vector3 rightClosedPos;

    public bool opening;

    void Awake()
    {
        leftDoor = transform.Find("LeftDoor").GetComponent<Rigidbody2D>();
        rightDoor = transform.Find("RightDoor").GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        leftOpenedPos = transform.Find("LeftOpenedPos").position;
        rightOpenedPos = transform.Find("RightOpenedPos").position;

        leftClosedPos = leftDoor.transform.position;
        rightClosedPos = rightDoor.transform.position;
    }

    void FixedUpdate()
    {
        if (opening)
        {
            Open();
        }
        else
        {
            Close();
        }
    }

    void Open()
    {
        RbMoveTowards(ref leftDoor, leftOpenedPos, openingSpeed);
        RbMoveTowards(ref rightDoor, rightOpenedPos, openingSpeed);
    }

    void Close()
    {
        RbMoveTowards(ref leftDoor, leftClosedPos, closingSpeed);
        RbMoveTowards(ref rightDoor, rightClosedPos, closingSpeed);
    }

    void RbMoveTowards(ref Rigidbody2D rb, in Vector2 to, in float speed)
    {
        Vector3 movePosition = rb.transform.position;

        movePosition.x = Mathf.MoveTowards(rb.transform.position.x, to.x, speed * Time.fixedDeltaTime);
        movePosition.y = Mathf.MoveTowards(rb.transform.position.y, to.y, speed * Time.fixedDeltaTime);

        rb.MovePosition(movePosition);
    }


}
