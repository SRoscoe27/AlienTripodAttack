using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
@author Sophia Roscoe
@version 2.0

This script is responsable for all UI screens within the game that provide the user an opportunity
to exit the game. 
Variables include:
    - A GameObject to store the GameOver image and its child components, this is needed to ensure 
        the Game Over screen appears
    - A GameObject to store the Success screen when the user beats the level, this is needed to ensure
        the Success screen and its child components appear
    - A GameObject to store the Pause menu and its child components to allow the user to pause the game
        when needed. 
    - A GameObject to store the AudioSource for the Pause menu to indicate that the user has paused the game
    - A GameObject to store the HUD and all its child components, this is used to toggle to HUD on and off
        when needed to ensure that it does not interfere with other UI screens.
**/

public class EndingScreenUI : MonoBehaviour
{
    public GameObject GOImage;
    public GameObject SuccessImage;
    public GameObject PauseImage;
    public GameObject Pause_Music;
    public GameObject HUD;
    // Start is called before the first frame update 
    void Start()
    {
    //Gets the AudioSource component so we can interact with it as such
       Pause_Music.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Public method designed to load/restart the main level. 
    // When enacted toggles the Game Over screen, sets the timeframe to 1 to ensure time is active and loads the main level
    // scene called "Level01". This is used by the Game Over and Success UI screens. 
    public void startLevel(){ 
        GOImage.active = !GOImage.active;
        Time.timeScale = 1;
        SceneManager.LoadScene("Level01");  
    } 

    //Public method designed to exit the main game once a button with the method is clicked.
    //When enabled it toggles the Game Over screen, sets the timeframe to 1 to move time forward
    //and loads the beginning scene called "IntroScreen". This is used for all UI screens.
    public void exitGame(){ 
        GOImage.active = !GOImage.active;
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScreen");  
    } 

    //Public method designed to allow the user to resume the level from a set point once the appropriate button has been clicked
    //This sets everything related to the pause menu to false and sets the timeframe to 1 to allow time to move forward
    public void resumeLevel(){
        Pause_Music.SetActive(false);
        Time.timeScale = 1;
        //Locks the cursor to the centre of the screen and makes it invisible. 
        Cursor.lockState = CursorLockMode.Locked; 
        Cursor.visible = false;
        HUD.active = !HUD.active;
        PauseImage.active = !PauseImage.active;

    }
}
