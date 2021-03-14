using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class platform : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject tile;
    public int width;
    public PhysicMaterial physicMaterial;
    private List<GameObject> tiles;
    List<float> tilesPositions = new List<float>();

    private bool hadneColliderchangeNow = false;
    
    void Start()
    {
        Vector3 startPosition = transform.position + Vector3.left*(((float)width-1)/2);
        for(int i = 0;i<width;i++)
        {
            GameObject childObject = Instantiate(tile,startPosition+ Vector3.right*i,Quaternion.identity) as GameObject;
            childObject.transform.parent = this.transform;
            tilesPositions.Add(startPosition.x + i - transform.position.x);
            
            
           
        }     
        UpdateColliders();
    }
    private void Update() {
        if(hadneColliderchangeNow){
            tilesPositions.Clear();
            for(int i =0; i<transform.childCount;i++)
            {
                tilesPositions.Add(transform.GetChild(i).position.x - transform.position.x);
            }
            UpdateColliders();
            hadneColliderchangeNow = false;
        }
    }
    public void HandleTileDestruction(){
        hadneColliderchangeNow = true;

    }
    private void UpdateColliders(){
        foreach (BoxCollider collider in gameObject.GetComponents<BoxCollider>())
        {    
            Destroy(collider);
        }      
        bool foundStart = false;
        bool foundEnd = false;
        float startPos = 0;
        float endPos = 0;
        for(int i =  0;i<tilesPositions.Count;i++)
        {
            if(!foundStart)
            {
                startPos = tilesPositions[i] - 0.5f;
                foundStart = true;
            }
            if(i+1<tilesPositions.Count)
            {
                if(tilesPositions[i] + 1.0 != tilesPositions[i+1])
                {
                    
                    endPos = tilesPositions[i] + 0.5f;
                    foundEnd = true;
                }
                else{
                    continue;
                }
            }
            else{
                
                endPos = tilesPositions[i] + 0.5f;
                foundEnd = true;
            }

            if(foundEnd && foundStart)
            {
                BoxCollider collider =  this.gameObject.AddComponent<BoxCollider>() as BoxCollider;
                collider.size = new Vector3(endPos-startPos,1,1);
                collider.center = new Vector3((startPos+endPos)/2,0,0);
                collider.material = physicMaterial;
                foundEnd = false;
                foundStart = false;
                startPos = 0;
                endPos = 0;
            }

        }
      


    }
}
