using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playermovement : MonoBehaviour
{
    [Header("Movement")]
    public float movespeed;

    public float groundDrag;

    public float jumpForce;
    public float jumpcooldown;
    public float airMultiplier;
    bool readyToJump = true;

    [Header("Keybinds")]
    public KeyCode jumpkey = KeyCode.Space;

    [Header("Ground Check")]
    public float playerHeight;
    public LayerMask WhatIsGround;
    bool grounded;

    public Transform orientation;

    float horizontalInput;
    float verticalInput;

    Vector3 moveDirection;
    Rigidbody rb;
   // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;
    }

    private void FixedUpdate()
    {
        MovePlayer();
    }
    private void MyInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInput = Input.GetAxisRaw("Vertical");
         
        //when to jump
        if(Input.GetKey(jumpkey) && readyToJump && grounded)
        {
            readyToJump = false;

            Jump();

            Invoke(nameof(ResetJump), jumpcooldown);
        }
    }
    // Update is called once per frame
    void Update()
    {
        // ground check
        grounded = Physics.Raycast(transform.position,Vector3.down , playerHeight * 0.5f + 0.2f, WhatIsGround);
        if(grounded)
        {
            Debug.Log("grounded");
        }
       
        MyInput();
        SpeedControl();

        //handle drag
        if(grounded) 
        {
          rb.drag = groundDrag;
        }
        else 
        {
            rb.drag = 0;
        }
    }

    private void MovePlayer()
    {
        // calaculate movement direction
        moveDirection = orientation.forward * verticalInput + orientation.right * horizontalInput;

        //on ground
        if (grounded)
        { rb.AddForce(moveDirection.normalized * movespeed * 10f, ForceMode.Force);
        }

        // in air
        else if(!grounded)
        {
            rb.AddForce(moveDirection.normalized * movespeed * 10f * airMultiplier, ForceMode.Force);
        }
    }

    private void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rb.velocity.x,0f,rb.velocity.z);
        // limit velocity if needed
        if(flatVel.magnitude > movespeed)
        {
            Vector3 limitedVel = flatVel.normalized * movespeed;
            rb.velocity = new Vector3(limitedVel.x, rb.velocity.y,limitedVel.z);
        }
    }

    private void Jump()
    {
        Debug.Log("Jump");
        // reset y velocity
        rb.velocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);
        
        rb.AddForce(transform.up * jumpForce,ForceMode.Impulse);
    }

    private void ResetJump()
    {
        Debug.Log("ResetJump");
        readyToJump = true;
    }
}
