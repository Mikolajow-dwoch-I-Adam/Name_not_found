using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class platformCreation : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public int width;
    private List<GameObject> tiles;
    
    void Start()
    {
        Vector3 startPosition = transform.position + Vector3.left*(((float)width-1)/2);
        BoxCollider collider = this.GetComponent<BoxCollider>();
        collider.size = new Vector3(width,1,1);
        for(int i = 0;i<width;i++)
        {
            GameObject childObject = Instantiate(tile,startPosition+ Vector3.right*i,Quaternion.identity) as GameObject;
            childObject.transform.parent = this.transform;
           
        }     
    }
}
