using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndingScreenUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void startLevel(){ 
        Time.timescale = 1;
        SceneManager.LoadScene("Level01");  
    } 

    public void exitGame(){ 
        Time.timescale = 1;
        SceneManager.LoadScene("IntroScreen");  
    } 
}
