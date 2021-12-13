using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@author Sophia Roscoe and premade code by Jim Ang
@version 1.0
The purpose of this script is to define the behavior of the powercell. In this script is the 
functionality as to when the powercells interact with the crates and enemy

Variables include: 
    - public GameObject explode: Used to store the explosion sound effect
    - public GameObject tripod: Used to store the script for triPodHealth so the reduceHealth() function can be accessed
    - public GameObject Crates: Used to reference the crates and its child objects
    - public float removeTime: float to store the amount of time we want the powercell to last before it is destroyed
**/

public class powerCell : MonoBehaviour
{
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
