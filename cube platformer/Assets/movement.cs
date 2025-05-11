using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class movement : MonoBehaviour
{
    public GameObject Player;
    public Rigidbody PlayerRigidbody;
    public int PlayerSpeed;
    // public Transform camtransform;
    // public Vector3 TargetVector;
    public float forceAmount=10;
    //  public Vector3 jumpmax;
    public float jumpheight =4.5f;
    public float groundheight;
    public float gravity= -9.18f;
    public Vector3 velocity;
    Boolean isGrounded;
    // public Transform groundCheck;
    //public float groundDistance=0.4f;
    //public LayerMask groundMask;
    Boolean isJumping;
    Boolean isfloating;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       if (Input.GetKey(KeyCode.W))
         { transform.Translate(Vector3.forward * PlayerSpeed * Time.deltaTime,Space.World); }
        if (Input.GetKey(KeyCode.A))
        { transform.Translate(Vector3.left * PlayerSpeed * Time.deltaTime,Space.World); }
        if (Input.GetKey(KeyCode.S))
        { transform.Translate(Vector3.back * PlayerSpeed * Time.deltaTime, Space.World); }
        if (Input.GetKey(KeyCode.D))
        { transform.Translate(Vector3.right * PlayerSpeed * Time.deltaTime, Space.World); }
        /*  if(Input.GetKey(KeyCode.Space) && isGrounded)                                                                  
          { //transform.Translate(Vector3.up * PlayerSpeed * Time.deltaTime, Space.World);
              PlayerRigidbody.velocity =new Vector3(0,forceAmount,0);
             // PlayerRigidbody.AddForce(Vector3.up * forceAmount,ForceMode.Impulse);
          }
        */
        /* if(Player.transform.position.y >= jumpmax.y)
         {
             PlayerRigidbody.AddForce(Vector3.down * forceAmount);
         }
         */

        //  velocity.y += gravity * Time.deltaTime * Time.deltaTime;



        /*  float x = Input.GetAxis("Horizontal");
          float z = Input.GetAxis("Vertical");


          Vector3 movement = new Vector3(x, 0, z);
          movement = Vector3.ClampMagnitude(movement, 1);
          transform.Translate(movement * PlayerSpeed * Time.deltaTime);
         */
        // TargetVector = Player.transform.position;
        // camtransform = Camera.main.transform;

        // isGrounded = Physics.CheckSphere(groundCheck.position,groundDistance,groundMask);

        // when player is at peak it is pushed downward for tight controls


      
        
        
     

    }
    private void FixedUpdate()
    {
        /*if (Input.GetKey(KeyCode.W))
         { PlayerRigidbody.AddForce(Vector3.forward * forceAmount, ForceMode.Impulse); }
         if (Input.GetKey(KeyCode.A))
         { PlayerRigidbody.AddForce(Vector3.left * forceAmount, ForceMode.Impulse); }
         if (Input.GetKey(KeyCode.S))
         { PlayerRigidbody.AddForce(Vector3.back * forceAmount, ForceMode.Impulse); }
         if (Input.GetKey(KeyCode.D))
         { PlayerRigidbody.AddForce(Vector3.right * forceAmount, ForceMode.Impulse); }

         if (Input.GetKey(KeyCode.Space))
         { PlayerRigidbody.AddForce(Vector3.up * forceAmount,ForceMode.Impulse); }
         */
       // jumpheight = jumpheight + PlayerRigidbody.position.y;
        if (Input.GetKey(KeyCode.Space) && isGrounded)
        {
           
            Debug.Log(jumpheight);
            //transform.Translate(Vector3.up * PlayerSpeed * Time.deltaTime, Space.World);
            PlayerRigidbody.velocity = new Vector3(0, forceAmount * -gravity, 0);
            // PlayerRigidbody.AddForce(Vector3.up * forceAmount,ForceMode.Impulse);
            isJumping= true;
            Debug.Log("isjumping");
            groundheight = PlayerRigidbody.position.y;
        }
        if (PlayerRigidbody.position.y > ((jumpheight + groundheight) * 0.8) && isJumping)
        {

            PlayerRigidbody.velocity = new Vector3(0, gravity * 0.1f , 0);
            isfloating = true;
            Debug.Log("isfloating=true");
        }
        if (PlayerRigidbody.position.y < ((jumpheight + groundheight) * 0.8) && isfloating)
        {
            PlayerRigidbody.velocity = new Vector3(0, gravity * 1.2f , 0);
            isfloating = false;
            Debug.Log("isfloating=false");

        }

    }
    

   private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log(" isGrounded = true");
            isGrounded = true;
            isJumping = false;
            


        }
    }
   
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Terrain"))
        {
            Debug.Log(" isGrounded = false");
            isGrounded = false;
            


        }
    }
 
   
    
}
