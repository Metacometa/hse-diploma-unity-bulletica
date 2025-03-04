using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 moveDir;
    public Vector2 aimDir;

    public bool onAttackButton;

    public void UpdateInput()
    {
        GetMovingInput();
        GetAimingInput();

        GetAttackInput();
    }

    void GetMovingInput()
    {
        moveDir.x = Input.GetAxisRaw("Horizontal");
        moveDir.y = Input.GetAxisRaw("Vertical");
    }

    void GetAimingInput()
    {
        onAttackButton = Input.GetMouseButtonDown(0);
    }

    void GetAttackInput()
    {
        aimDir = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

}
