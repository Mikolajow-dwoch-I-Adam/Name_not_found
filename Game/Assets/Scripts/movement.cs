using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    public float acceleration = 20;
    public float maxHoorizontalSpeed = 10;
    public float breakAndLowSpeedAccBoost = 10;
    //maksymalna i minimalna predkość przy której zwiekszane jest przyśpieszenie
    public float LowSpeedTreshHold = 3;
    public float maxSpeedTreshHold = 20;
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
                float targetBreakingSpeed = rb.velocity.x - rb.velocity.x/Mathf.Abs(rb.velocity.x)*1*Time.deltaTime*Friction;
                if(targetBreakingSpeed/rb.velocity.x <0)
                {
                    targetBreakingSpeed = 0;
                }
                rb.velocity = new Vector3(targetBreakingSpeed,rb.velocity.y,0);    
            }

        }
        else{
            if(grounded || accelerateWhileMidAir)
            {
                
                float targetSpeed;
                if(rb.velocity.x*direction < LowSpeedTreshHold && rb.velocity.x*direction >-maxSpeedTreshHold)
                {
                    targetSpeed = rb.velocity.x + direction*acceleration*Time.deltaTime*breakAndLowSpeedAccBoost;
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
