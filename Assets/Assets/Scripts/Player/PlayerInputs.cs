using UnityEngine;

public class PlayerInputs : MonoBehaviour
{
    [HideInInspector] public Vector2 aiming;
    [HideInInspector] public Vector2 movement;

    [HideInInspector] public bool attacking;

    private void Update()
    {
        GetMovingInput();
        GetAimingInput();

        GetAttackInput();
    }

    void GetMovingInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void GetAimingInput()
    {
        attacking = Input.GetMouseButtonDown(0);
    }

    void GetAttackInput()
    {
        Vector2 mousePos = Input.mousePosition;

        aiming = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 10));
    }
}
