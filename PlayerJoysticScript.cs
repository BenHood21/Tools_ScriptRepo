using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManagerScript : MonoBehaviour
{
    // component
    private CharacterController charController;
    private Animator animator;
    private JoystickManager _joystick;

    //
    private Transform meshPlayer;

    // move
    private float inputX;
    private float inputZ;
    private Vector3 playerMovement;
    private float moveSpeed;
    private float gravity;

    // Start is called before the first frame update
    void Start()
    {
        moveSpeed = 0.25f;
        gravity = .5f;
        GameObject tempPlayer = GameObject.FindGameObjectWithTag("Player");
        charController = tempPlayer.GetComponent<CharacterController>();
        meshPlayer = tempPlayer.transform.GetChild(0);
        animator = meshPlayer.GetComponent<Animator>();
       _joystick = GameObject.Find("imgJoystickBg").GetComponent<JoystickManager>();
    }

    // Update is called once per frame
    void Update()
    {
       // inputX = Input.GetAxis("Horizontal");
        //inputZ = Input.GetAxis("Vertical");
        inputX = _joystick.inputHorizontal();
        inputZ = _joystick.inputVertical();
        //Debug.Log(inputX + inputZ);
        if (inputX == 0 && inputZ == 0)
        {
            animator.SetBool("Walking_B", false);
        }
        else
        {
            animator.SetBool("Walking_B", true);

        }

        if (Input.GetButtonDown("Fire1"))
        {
           // animator.SetBool("DoubleAttacking_B",true);
        }
        else
        {
           // animator.SetBool("DoubleAttacking_B",false);
        }
    }
    
    public void DoubleSwing()
    {
        animator.SetBool("DoubleAttacking_B",true);
        Debug.Log("Double Swinging");
    }

    public void StopDoubleSwinging()
    {
        animator.SetBool("DoubleAttacking_B", false);
        Debug.Log("Stop Double Swinging");
    }

    public void UpSwing()
    {
        animator.SetBool("UpSwing_B",true);
        Debug.Log("Up Swinging");
    }

    public void StopUpSwing()
    {
        animator.SetBool("UpSwing_B", false);
        Debug.Log("Stop Up Swinging");
    }
    
    public void QuickSwing()
    {
        animator.SetBool("QuickSwing_B",true);
        Debug.Log("Up Swinging");
    }

    public void StopQuickSwing()
    {
        animator.SetBool("QuickSwing_B", false);
        Debug.Log("Stop Up Swinging");
    }
    
    private void FixedUpdate()
    {
        // gravity
        if (charController.isGrounded)
        {
            playerMovement.y = 0f;
        }
        else
        {
            playerMovement.y -= gravity * Time.deltaTime;
        }

        //movement
        playerMovement = new Vector3(inputX * moveSpeed, playerMovement.y, inputZ * moveSpeed);
        charController.Move(playerMovement);

        //mesh rotate
        if (inputX != 0 || inputZ != 0)
        {
            Vector3 lookDir = new Vector3(playerMovement.x, 0, playerMovement.z);
            meshPlayer.rotation = Quaternion.LookRotation(lookDir);

        }
    }
}
