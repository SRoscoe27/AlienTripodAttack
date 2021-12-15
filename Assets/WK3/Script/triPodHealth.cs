using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@Sophia Roscoe and Jim Ang
@version 1.0
The purpose of this script is to define the tripods health and what happens when the health equals 0
This is useful as it provides our user with a method to complete the game. 

Variables include: 
    - public float health: Used to store the health of the tripod boss
    - public GameObject smoke, flare: GameObjects storing the particle effects of smoke and flare sparks respectively.
        These are used to indicate that the tripod has been destroyed
    - public GameObject SuccessImage: A GameObject to store the Success screen when the user beats the level, this is needed to ensure
        the Success screen and its child components appear
    - public AudioClip successSound: AudioCli that is played with the success menu to indicate that the user has completed the game
    - public GameObject HUD: A GameObject to store the HUD and all its child components, this is used to toggle to HUD on and off
        when needed to ensure that it does not interfere with other UI screens.
    
**/

public class triPodHealth : MonoBehaviour
{
    private float health = 3; 
    public GameObject smoke, flare; 
    public GameObject SuccessImage;
    public AudioClip successSound;
    public GameObject HUD;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Decrease health when tripod is hit. 
    public void reduceHealth(){ 
        health --; 
    }

    //For each collision, check if health is 0 and activate appropriate GameObjects if so. 
    void OnCollisionEnter(Collision other){
        if (health <= 0){ 
            smoke.SetActive(true); 
            flare.SetActive(true); 

            //Time is frozen so the game does not impact the screens, The cursor is then unlocked and viable to be able to navagate the screen
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None; 
            Cursor.visible = true; 

            //plays success sound and toggles to HUD and SuccessMenu to make the latter visiable
            AudioSource.PlayClipAtPoint(successSound, transform.position);
            HUD.active = !HUD.active;
            SuccessImage.active = !SuccessImage.active; 
        } 
    }

    // Update is called once per frame
    void Update () { 
        
    } 
}
