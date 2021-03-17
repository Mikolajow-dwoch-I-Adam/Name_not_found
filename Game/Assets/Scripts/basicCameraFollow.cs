using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class basicCameraFollow : MonoBehaviour
{
    //nie ma co tłumaczyć najprostrzy skrypt kontrolujący kamere
    public GameObject ObjectToFollow;
    public Vector3 Offset;

    void Update()
    {   
        transform.position =ObjectToFollow.transform.position +Offset;
    }
}
