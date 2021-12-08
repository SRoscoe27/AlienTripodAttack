using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingScreenUI : MonoBehaviour
{
    public GameObject GOImage;
    public GameObject SuccessImage;
    public GameObject PauseImage;
    // Start is called before the first frame update 
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startLevel(){ 
        GOImage.active = !GOImage.active;
        Time.timeScale = 1;
        SceneManager.LoadScene("Level01");  
    } 

    public void exitGame(){ 
        GOImage.active = !GOImage.active;
        Time.timeScale = 1;
        SceneManager.LoadScene("IntroScreen");  
    } 

    public void resumeLevel(){
        PauseImage.active = !PauseImage.active;
        Time.timeScale = 1;

    }
}
