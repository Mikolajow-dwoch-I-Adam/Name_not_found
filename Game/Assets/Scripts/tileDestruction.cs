using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDestruction : MonoBehaviour
{
    public GameObject particles;
    
    public void DestroyTile(){
        
        Destroy(this.gameObject);
        transform.parent.GetComponent<platform>().HandleTileDestruction();
        Instantiate(particles,transform.position,Quaternion.identity);
        
    }
}
