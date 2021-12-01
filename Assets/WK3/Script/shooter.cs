using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shooter : MonoBehaviour
{

    public GameObject powercell; //link to the powerCell prefab 
    public int no_cell; //number of powerCell owned 
    public AudioClip throwSound; //throw sound 
    public float throwSpeed= 20;//throw speed  
    public AudioClip collectionSound; // sound when power cell is collected
    private Rigidbody rb;

    public GameObject GOImage; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        //GameObject.Find("GOCanvas");
        //GOImage.GetComponent<Image>();
    }

    // Code for picking up power cell collectables 
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pick Up")){
            Debug.Log("Triggered");
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectionSound, transform.position);
            no_cell ++;
        }

        // if user dies a game over screen need to occur - Ben your alien work would join with me here ^-^
        else if (other.gameObject.CompareTag("Alien")){
            Debug.Log("Noooo an alien hit me i die now");
            AudioSource.PlayClipAtPoint(collectionSound, transform.position);
            Time.timeScale = 0; 
            GOImage.active = !GOImage.active;

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
