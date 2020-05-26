using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickController : MonoBehaviour
{
    private PlayerController playerController;

    private Animator anim;

    void Awake(){
        playerController = GetComponent<PlayerController>();
    }

    public void MoveLeft()
    {
        playerController.MoveLeft();
    }

    public void MoveRight()
    {
        playerController.MoveRight();
    }

    public void PlayerJump()
    {
        playerController.PlayerJump();
    }

}
