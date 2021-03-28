using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class booletDestructionOnHit : MonoBehaviour
{
    public GameObject patricles;
    public float explosionRadius =0;
    public float explosionForce =0;

    private void OnCollisionEnter(Collision other) {
        Collider[] colliders = Physics.OverlapSphere(transform.position,explosionRadius);
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
                Vector3 direction = collider.transform.position - transform.position;
                collider.gameObject.GetComponent<Rigidbody>().velocity+=direction.normalized*explosionForce;
            }
        }
        Instantiate(patricles,transform.position,Quaternion.identity);
        Destroy(this.gameObject);
    }
}
