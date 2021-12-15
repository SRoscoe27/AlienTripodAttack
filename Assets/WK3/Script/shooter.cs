using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@author Sophia Roscoe and previous predefined work
@version 2.0
The purpose of this script is to give to control the physics and functionality of the user and its 
child compoants including the player camera. This included functionality that causes the UI screens to appear
as their conditions are based on the user. 

Variables include: 
    - public GameObject powercell: GameObject to store the powercell prefab
    - public int no_cell: Number of powercells owned 
    - public float throwspeed: Speed at powercell is thrown
    - public AudioClip throwSound: Sound when powercell is thrown
    - public AudioClip collectionSound: Sound when powercell is collected 
    - public AudioClip gameOver: Sound when game over screen is enabled
    - private Rigidbody rb: Provides a rigidbody for us to add force to. This allows us to create the propulsion/physics 
        needed for the jetpack to function.
    - public GameObject GOImage: A GameObject to store the GameOver image and its child components, this is needed to ensure 
        the Game Over screen appears
    - A GameObject to store the Pause menu and its child components to allow the user to pause the game
        when needed. 
    - A GameObject to store the AudioSource for the Pause menu to indicate that the user has paused the game
    - A GameObject to store the HUD and all its child components, this is used to toggle to HUD on and off
        when needed to ensure that it does not interfere with other UI screens.
**/ 

public class shooter : MonoBehaviour
{
    public GameObject pistol;
    public GameObject powercell; //link to the powerCell prefab 
    public GameObject rifleBeacon;
    public int no_cell; //number of powerCell owned 
    public float throwSpeed= 20;//throw speed  
    public bool isRifle;
    public AudioClip throwSound; //throw sound 
    public AudioClip gunCock;
    public AudioClip collectionSound; // sound when power cell is collected
    public AudioClip gameOver;
    private Rigidbody rb;

    public GameObject GOImage; 
    public GameObject PauseImage;
    public GameObject Pause_Music; 
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the rigidbody component and defines the default state for the pause menu and HUD.
        rb = GetComponent<Rigidbody>();
        Pause_Music.GetComponent<AudioSource>();
        Pause_Music.SetActive(false);
        HUD.active = true; 
    }

    // Code for picking up power cell collectables and zombie interaction
    void OnTriggerEnter(Collider other){
        Debug.Log("Triggered");
        if(other.gameObject.CompareTag("Pick Up")){
            other.gameObject.SetActive(false);
            AudioSource.PlayClipAtPoint(collectionSound, transform.position);
            no_cell ++;
        }

        // Code for when zombie touches player
        else if (other.gameObject.CompareTag("Alien")){
            Debug.Log("Noooo an alien hit me i die now");
            //Play game over music and freeze time so no interactions in the game affect the following
            AudioSource.PlayClipAtPoint(gameOver, transform.position);
            Time.timeScale = 0; 
            // Allow the mouse to move and make it visiable
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;
            //toggle the HUD and Game Over screens to make the GO image visiable without the HUD
            HUD.active = !HUD.active; 
            GOImage.active = !GOImage.active;

        }
        else if(other.gameObject.CompareTag("RiflePickup")){
            isRifle = true;
            pistol.GetComponent<GunController>().SwapToRifle();
            AudioSource.PlayClipAtPoint(gunCock, transform.position);
            rifleBeacon.SetActive(false);
            other.gameObject.SetActive(false);
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

        //Code for when the user wishes to pause the game 
        else if(Input.GetKeyDown(KeyCode.P)){
            // Unlock the mouse so the user can move it around and make it visiable
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true;

            // Toggle the HUD and Pause menu to make the pause menu present without the HUD present
            HUD.active = !HUD.active;
            Pause_Music.active = !Pause_Music.active;
            PauseImage.active = !PauseImage.active;
            // freeze time so gameplay doesnt interfere 
            Time.timeScale = 0;

        }
    }
}
