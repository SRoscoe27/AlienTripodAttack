using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class shooter : MonoBehaviour
{
    public GameObject pistol;
    public GameObject powercell; //link to the powerCell prefab 
    public int no_cell; //number of powerCell owned 
    public AudioClip throwSound; //throw sound 
    public float throwSpeed= 20;//throw speed  
    public AudioClip collectionSound; // sound when power cell is collected
    public AudioClip gameOver;
    private Rigidbody rb;

    public GameObject GOImage; 
    public GameObject PauseImage;
    public GameObject Pause_Music; 

    public static int sceneCount; 

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Pause_Music.GetComponent<AudioSource>();
        Pause_Music.SetActive(false);
        Debug.Log(sceneCount);
    }

    // Code for picking up power cell collectables 
    void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Pick Up")){
            Debug.Log("Triggered");
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectionSound, transform.position);
            no_cell ++;
        }

        else if (other.gameObject.CompareTag("Alien")){
            Debug.Log("Noooo an alien hit me i die now");
            AudioSource.PlayClipAtPoint(gameOver, transform.position);
            Time.timeScale = 0; 
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            GOImage.active = !GOImage.active;

        }
        if(other.gameObject.CompareTag("RiflePickup")){
            pistol.GetComponent<GunController>().SwapToRifle();
            Destroy(other); 
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
        else if(Input.GetKeyDown(KeyCode.P)){
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            Pause_Music.active = !Pause_Music.active;
            PauseImage.active = !PauseImage.active;
            Time.timeScale = 0;

        }
    }
}
