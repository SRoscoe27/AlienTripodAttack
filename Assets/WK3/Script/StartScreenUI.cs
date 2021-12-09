using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject HTPPanel;   
    // Start is called before the first frame update
    void Start()
    {
        //HTPPanel.GetComponent<Image>(); 

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartLevel(){ 
 
        SceneManager.LoadScene("Level01");  
    } 

    public void howToPlay(){
        HTPPanel.active = !HTPPanel.active;
         
    } 

    public void exitLevel(){ 
 
        Application.Quit(); 
    } 
}
