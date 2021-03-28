using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDash : MonoBehaviour
{
    private Camera cam;
    public GameObject fulyLoadedParticles;
    private GameObject currentParticles;
    private bool spawnedParticles = false;
    public float timeToFullyCharge = 4.0f;
    public float minDashVelocity = 2.0f;
    public float maxDashVelocity = 8.0f;
    public float hitVelocity = 15.0f;
    public float radiusOfDestruction = 1.0f;
    public float fullyLoadedDashLenght = 1.0f;
    private float timeCharged = 0;
    public float cooldown = 1.0f;
    private float cooldownLeft;
    private Rigidbody rb;
    private float dashTimeLeft;
    private bool curentlyDashing;
    
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        cooldownLeft = 0;
        cam = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownLeft -= Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            if(!spawnedParticles)
            {
                if(timeCharged>=timeToFullyCharge)
                {
                    currentParticles = Instantiate(fulyLoadedParticles,transform.position,Quaternion.identity) as GameObject;
                    currentParticles.transform.parent = this.transform;
                    spawnedParticles = true;
                }
            }
            timeCharged += Time.deltaTime;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            if(cooldownLeft<=0)
            {
                
                Vector3 desireddirection =findCursorDirection();

                if(timeCharged>=timeToFullyCharge)
                {
                    curentlyDashing = true;
                    dashTimeLeft = fullyLoadedDashLenght;
                }
                else{
                    rb.velocity = desireddirection*Mathf.Lerp(minDashVelocity,maxDashVelocity,Mathf.Clamp(timeToFullyCharge,0,timeToFullyCharge));
                }

                
                
                

                //reset zmiennych kontrolujacych dasha
                timeCharged = 0;
                cooldownLeft = cooldown;

                //zniszczenie efektów
                Destroy(currentParticles);               
                spawnedParticles = false;
            }
            
        }
        if(curentlyDashing){
            if(dashTimeLeft >0)
            {
                Vector3 desiredDirection = findCursorDirection();
                rb.velocity = desiredDirection*maxDashVelocity;
                dashTimeLeft-= Time.deltaTime;
            }
            else{
                curentlyDashing = false;
            }
        }
        
    }
    private void OnCollisionEnter(Collision other) {
        if(curentlyDashing){
            Collider[] colliders = Physics.OverlapSphere(transform.position,radiusOfDestruction);
            foreach(Collider collider in colliders){
                if(collider.tag == "Tile")
                {
                    collider.gameObject.GetComponent<tileDestruction>().DestroyTile();
                }
                else if(collider.tag == "Tile_collider")
                {
                    collider.transform.parent.gameObject.GetComponent<tileDestruction>().DestroyTile();
                }
                else if(collider.gameObject.GetComponent<Rigidbody>()){
                Vector3 diection = rb.velocity.normalized;
                collider.gameObject.GetComponent<Rigidbody>().velocity += diection*hitVelocity;
                }
            }
        }
       
    }

    Vector3 findCursorDirection(){
                //znalezienie pozycji kurosra w odniesieniu do kooradynatow swiata
                Vector3 point = new Vector3();
                Vector2 mousePos = new Vector2();           
                mousePos.x = Input.mousePosition.x;
                mousePos.y =  Input.mousePosition.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 30));

                //obliczenie kierunku na podstawie znalezionego punktu
                return  Vector3.Normalize(point - transform.position);

    }
}
