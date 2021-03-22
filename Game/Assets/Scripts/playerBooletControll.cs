using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerBooletControll : MonoBehaviour
{

    private Camera cam;
    void Start()
    {
        cam = Camera.main;
    }
    public GameObject boolet;
    public float minBooletVelocity = 10;
    public float maxBooletVelocity = 40;
    public float timeToFullyCharge = 4.0f;
    private float timeCharged = 0;
    void Update() {
        if(Input.GetMouseButton(0))
        {
            timeCharged += Time.deltaTime;
        }
        
        if(Input.GetMouseButtonUp(0))
        {
            Vector3 point = new Vector3();
           
            Vector2 mousePos = new Vector2();

           
            mousePos.x = Input.mousePosition.x;
            mousePos.y =  Input.mousePosition.y;
            
            Debug.Log(mousePos);
            point = cam.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 30));
            Vector3 desireddirection = Vector3.Normalize(point - transform.position);
            GameObject booletCreated = Instantiate(boolet,transform.position,Quaternion.identity) as GameObject;
            booletCreated.GetComponent<Rigidbody>().velocity = desireddirection*Mathf.Lerp(minBooletVelocity,maxBooletVelocity,Mathf.Clamp(timeCharged,0,timeToFullyCharge));
            timeCharged = 0;
        }
    }
}
