using TMPro;
using UnityEngine;
using UnityEngine.Windows;
using static UnityEngine.GraphicsBuffer;

public class Player : Gunman
{
    private InputManager input;

    protected override void Awake()
    {
        base.Awake();
        input = GetComponent<InputManager>();
    }

    protected override void FixedUpdate()
    {
        /*        if (!health.isLostControl)
                {
                    move.Move(ref rb, input.moveDir);
                    shooting.RotateGun(input.aimDir);

                    if (!shooting.onAttack)
                    {
                        if (shooting.IsMagazineEmpty())
                        {
                            if (!shooting.onReload)
                            {
                                shooting.ReloadManager();
                            }

                        }
                        else
                        {
                            if (!shooting.onCooldown && !shooting.onReload)
                            {
                                //StartCoroutine(gun.ShootManager());
                            }
                        }
                    }

                    if (input.onAttackButton)
                    {
                        shooting.ShootingManager(input.aimDir);
                    }
                }*/

        //Vector2 dir = targetPosition - (Vector2)transform.position;

        move.Move(ref rb, input.moveDir);
        shooting.RotateGun(input.aimDir - (Vector2)transform.position);

        /*        if (!shooting.onAttack)
                {
                    if (shooting.IsMagazineEmpty())
                    {
                        if (!shooting.onReload)
                        {
                            shooting.ReloadManager();
                        }

                    }
                    else
                    {
                        if (!shooting.onCooldown && !shooting.onReload)
                        {
                            //StartCoroutine(gun.ShootManager());
                        }
                    }
                }*/

    }

    protected override void Update() 
    {
        input.UpdateInput();

        if (input.onAttackButton)
        {
            shooting.ShootingManager();
        }

    }
}
