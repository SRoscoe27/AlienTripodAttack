using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerCell : MonoBehaviour
{
//comment remove later
    public GameObject explode; 
    private GameObject tripod; 
    private GameObject Crates;
    float removeTime = 3.0f; 
    // Start is called before the first frame update
    void Start()
    {
        tripod = GameObject.Find ("tripod");//find the tripod   
        Crates = GameObject.Find ("Crates"); //find the crates parent object 
        Destroy(gameObject, removeTime); //destory the object after 2s 
    }

    //Collision code for the tripod enemy and crates 
    void OnCollisionEnter(Collision other) { 
         
        if (other.gameObject.tag == "Enemy") { 
            //instantiate the explosion  
            Instantiate(explode, transform.position, transform.rotation); 
 
            //reduce the tripod's health 
            tripod.GetComponent<triPodHealth>().reduceHealth(); 
            Destroy(gameObject);//destory self  
        } 

        else if (other.gameObject.tag == "Crates"){
            Instantiate(explode, transform.position, transform.rotation); 
            //change isKinematic to false for all objects in the parent Crates object
            foreach(var rb in Crates.GetComponentsInChildren<Rigidbody>()){
                rb.isKinematic = false;
            }
            Destroy(gameObject);//destory self  
        }
         
    }

    void OnDestroy(){
        Instantiate(explode, transform.position, transform.rotation); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
