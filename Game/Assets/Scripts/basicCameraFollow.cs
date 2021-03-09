using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCameraFollow : MonoBehaviour
{
    public GameObject ObjectToFollow;
    public Vector3 Offset;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
        transform.position =ObjectToFollow.transform.position +Offset;
    }
}
