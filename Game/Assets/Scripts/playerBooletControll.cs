using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBooletControll : MonoBehaviour
{

    private Camera cam;
    
    public GameObject boolet;
    public float minBooletVelocity = 10;
    public float maxBooletVelocity = 100;
    public float timeToFullyCharge = 4.0f;
    public float timeToCreateExplosion = 2.0f;
    public float minExplosionRadius = 1.5f;
    public float maxExplosionRadius = 2.0f;
    public float minExplosionForce = 3.0f;
    public float maxExplosionForce = 8.0f;
    public float recoilFactor = 0.25f; 
    private float timeCharged = 0;
    public float cooldown = 1.0f;
    private float cooldownLeft;
    void Start()
    {
        cooldownLeft = 0;
        cam = Camera.main;
    }
    void Update() {
        cooldownLeft -= Time.deltaTime;
        if(Input.GetMouseButton(0))
        {
            timeCharged += Time.deltaTime;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            if(cooldownLeft<=0)
            {
                //znalezienie pozycji kurosra w odniesieniu do kooradynatow swiata
                Vector3 point = new Vector3();
                Vector2 mousePos = new Vector2();           
                mousePos.x = Input.mousePosition.x;
                mousePos.y =  Input.mousePosition.y;
                point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 30));

                //obliczenie kierunku na podstawie znalezionego punktu
                Vector3 desireddirection = Vector3.Normalize(point - transform.position);

                //stworzenie pocisku i zapisanie odniesienie do niego
                GameObject booletCreated = Instantiate(boolet,transform.position,Quaternion.identity) as GameObject;

                //ustawianie odpowiedniego zasiegu eksplozji i siły pocisku
                if(timeCharged>=timeToCreateExplosion){
                    booletDestructionOnHit booletScript= booletCreated.GetComponent<booletDestructionOnHit>();
                    booletScript.explosionRadius = Mathf.Lerp(minExplosionRadius,maxExplosionRadius,Mathf.Clamp(timeCharged,0,timeToFullyCharge));
                    booletScript.explosionForce = Mathf.Lerp(minExplosionForce,maxExplosionForce,Mathf.Clamp(timeCharged,0,timeToFullyCharge));
                }
                
                
                //nadanie pociskowi i graczowi odpowiedniej predkości
                Vector3 changeOfSpeed = desireddirection*Mathf.Lerp(minBooletVelocity,maxBooletVelocity,Mathf.Clamp(timeCharged,0,timeToFullyCharge));
                booletCreated.GetComponent<Rigidbody>().velocity = changeOfSpeed;
                GetComponent<Rigidbody>().velocity -=  changeOfSpeed*recoilFactor;

                //reset zmiennych kontrolujacych eksplozje
                timeCharged = 0;
                cooldownLeft = cooldown;
            }
            
        }
    }
}
