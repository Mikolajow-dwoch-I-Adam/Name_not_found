using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyAfterDelay : MonoBehaviour
{
     public float delay;
    private float timeLeft;
    void Start()
    {
        timeLeft = delay;        
    }

    // Update is called once per frame
    void Update()
    {
        timeLeft -= Time.deltaTime;
        if(timeLeft<=0)
        {
            Destroy(this.gameObject);
        }
    }
}
