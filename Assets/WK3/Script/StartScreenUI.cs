using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
@author Sophia Roscoe
@version 1.0
The purpose of the script is to control the functionality of the introduction UI screen and the 
appropriate buttons to navigate the UI.

Variables include: 
    - public GameObject HTPPanel: GameObject used to store the How To Play panel and its child components.
        When the user presses the appropriate button this is made active. 
    - public GameObject Menu_Music: This stores the AudioSource music for the menu/intro screen 
**/

public class StartScreenUI : MonoBehaviour
{
    public GameObject HTPPanel;   
    public GameObject Menu_Music;

    // Start is called before the first frame update
    void Start()
    {
        //Gets the AudioSource component and sets it to true so it plays once the screen is opened
        Menu_Music.GetComponent<AudioSource>();
        Menu_Music.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Function to load the main level. This is performed by the first button on the menu.
    //This turns off the music and loads the scene for Level01
    public void restartLevel(){ 
        Menu_Music.SetActive(false);
        SceneManager.LoadScene("Level01");  
    } 

    //This is a function to display a small how to play page detailing how to play the game.
    //This is done by making the HTPPanel active when the appropriate button is clicked 
    public void howToPlay(){
        HTPPanel.active = !HTPPanel.active;
    } 

    // This function exits the game entirely once the appropriate button is clicked
    public void exitLevel(){ 
        Application.Quit(); 
    } 
}
