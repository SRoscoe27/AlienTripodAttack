using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shooter : MonoBehaviour
{

    public GameObject powercell; //link to the powerCell prefab 
    public int no_cell; //number of powerCell owned 
    public AudioClip throwSound; //throw sound 
    public float throwSpeed= 20;//throw speed  
    public AudioClip collectionSound; // sound when power cell is collected
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Code for picking up power cell collectables 
    void OnTriggerEnter(Collider other){
        Debug.Log("Triggered");
        if(other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectionSound, transform.position);
            no_cell ++;
        }

    }

    // Update is called once per frame
    void Update()
    {
        //if left control (fire1) pressed, and we still have at least 1 cell  
        if (Input.GetButtonDown ("Fire1") && no_cell > 0) {  
 
            no_cell --; //reduce the cell 
 
            //play throw sound 
            AudioSource.PlayClipAtPoint(throwSound, transform.position);  
 
            //instantaite the power cel as game object 
            GameObject cell = Instantiate(powercell, transform.position, transform.rotation) as GameObject; 
 
            //ask physics engine to ignore collison between  
            //power cell and our FPSControler 
            Physics.IgnoreCollision(transform.root.GetComponent<Collider>(),  
                                    cell.GetComponent<Collider>(), true); 
 
            //give the powerCell a velocity so that it moves forward 
           cell.GetComponent<Rigidbody>().velocity = transform.forward * throwSpeed;  
        } 
    }
}
