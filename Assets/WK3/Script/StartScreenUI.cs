using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartScreenUI : MonoBehaviour
{
    public GameObject HTPPanel;   
    public GameObject Menu_Music;
    // Start is called before the first frame update
    void Start()
    {
        Menu_Music.GetComponent<AudioSource>();
        Menu_Music.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void restartLevel(){ 
        Menu_Music.SetActive(false);
        SceneManager.LoadScene("Level01");  
    } 

    public void howToPlay(){
        HTPPanel.active = !HTPPanel.active;
         
    } 

    public void exitLevel(){ 
 
        Application.Quit(); 
    } 
}
