using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{

    /*
        działa to tak, jest przyspieszenie.
        I to przyśpieszenie jest swiększane breakAndLowSpeedAccBoost razy kiedy
            a) chesz zmienieć kierunek ruchu
            b) masz prędkość mniejszą niż LowSpeedTreshHold
        Note: kiedy twoja prędkość jest większa niż maxSpeedTreshHold bonus nie zostanie nałożony jeżeli prędkość jest zbyt duża
        bool o nazwie accelerateWhileMidAir kiedy ustawiony na false sprawia że można przśpieszać tylko jeżeli stoimy na ziemi
        midAirJumpNumber to ilość skoków które można wykonać po oderwaniu od ziemi



    */
    public float acceleration = 20;
    public float maxHoorizontalSpeed = 15;
    public float breakAndLowSpeedAccBoost = 8;
    public float LowSpeedTreshHold = 3;
    public float maxSpeedTreshHold = 20;
    private float Friction = 10.0f;
    public int midAirJumpNumber = 1;
    public float jumpVelocity = 10;
    public bool accelerateWhileMidAir = true;
    private int jumpsLeft;

    private Rigidbody rb;
    private bool grounded = false;

    //sprawdzanie czy gracz jest na ziemi
    //Każdy kawelek ma nad sobą dość cienki collider ustawiony na triger czyli można w niego wejść
    //Jest to rozwiązane w ten sposób żeby dało się załadować skoki tylko kiedy stoimy na kafelku a nie uderzamy w niego od dołu
    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.tag == "Tile"){
            grounded = true;
            jumpsLeft = midAirJumpNumber;
        }
    }
    private void OnTriggerExit(Collider other) {
        if(other.gameObject.tag == "Tile"){
            grounded = false;
        }
    }
    void Start()
    {
        //ustalenie parametrów startowych
        jumpsLeft = midAirJumpNumber;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        //sprawdzenie w którą stronę chce poruszać się gracz direction ==  0 również gdy wciśnięte są oba przyciski
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

                //nałożenie tarcia, nie używam systemu unity ponieważ w ten sposób tarcie można nałożyć tylko niedy gracz nie chce się poruszać
                //Kiedy tarcie jest nałożone gdy gracz przyśpiesza to faktyczne przyśpieszenie jest mniejsze od pożądanego

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
                //sprawdzenie czy powinien zostać nałożony bonus do prędkości
                if(rb.velocity.x*direction < LowSpeedTreshHold && rb.velocity.x*direction >-maxSpeedTreshHold)
                {
                    targetSpeed = rb.velocity.x + direction*acceleration*Time.deltaTime*breakAndLowSpeedAccBoost;
                }
                else{
                    targetSpeed = rb.velocity.x + direction*acceleration*Time.deltaTime;
                }
                
                if(direction*targetSpeed <= maxHoorizontalSpeed)
                {
                    rb.velocity = new Vector3(targetSpeed, rb.velocity.y,0);
                } 

            }

        }
       
        if(Input.GetKeyDown("w") || Input.GetKeyDown("space"))
        {
            //prosty system skoków, kontroluje się prędkość nadaną a nie wysokość skoku
            if(jumpsLeft > 0)
            {
                rb.velocity = new Vector3(rb.velocity.x,jumpVelocity,0);
                if(!grounded){
                    jumpsLeft--;
                }
            }
        }
    }
}
