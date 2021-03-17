using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tileDestruction : MonoBehaviour
{
    public GameObject particles;
    
    public void DestroyTile(){
        //zniszcz obiekt, wywołaj funkcje o zaktualizowanie colliderów i strórz podstawowy efekt
        Destroy(this.gameObject);
        transform.parent.GetComponent<platform>().HandleTileDestruction();
        Instantiate(particles,transform.position,Quaternion.identity);
        
    }
}
