using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float acceleration = 5;
    public float maxHoorizontalSpeed = 10;
    public float InstatnAccelerationMaxSpeed = 3;
    private float Friction = 10.0f;
    public int jumpNumber = 2;
    public float jumpVelocity = 10;
    public bool accelerateWhileMidAir = true;
    private int jumpsLeft;

    private Rigidbody rb;
    private bool grounded = false;

    //sprawdzanie czy gracz jest na ziemi
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Tile"){
            grounded = true;
            jumpsLeft = jumpNumber;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Tile"){
            grounded = false;
        }
    }
    void Start()
    {
        jumpsLeft = jumpNumber;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float direction=0;
        if(Input.GetKey("a"))
        {
            direction --;
        }
        if(Input.GetKey("d"))
        {
            direction++;
        }
        if(direction==0)
        {
            if(grounded && rb.velocity.x !=0)
            {
               rb.velocity -= rb.velocity.x/Mathf.Abs(rb.velocity.x)*Vector3.right*Time.deltaTime*Friction; 
            }

        }
        else{
            if(grounded || accelerateWhileMidAir)
            {
                float targetSpeed;
                if(rb.velocity.x*direction < InstatnAccelerationMaxSpeed)
                {
                    targetSpeed = direction*InstatnAccelerationMaxSpeed;
                }
                else{
                    targetSpeed = rb.velocity.x +direction*acceleration*Time.deltaTime;
                }
                
                if(Mathf.Abs(targetSpeed) <= maxHoorizontalSpeed)
                {
                    rb.velocity = new Vector3(targetSpeed, rb.velocity.y,0);
                } 

            }

        }
       
        if(Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            if(jumpsLeft > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x,jumpVelocity,0);
                jumpsLeft--;
            }


            
            

        }




    }
}
