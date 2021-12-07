using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triPodHealth : MonoBehaviour
{
    private float health = 3; 
    public GameObject smoke, flare; 
    public GameObject SuccessImage;
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
            Time.timeScale = 0; 
            SuccessImage.active = !SuccessImage.active; 
        } 
    }

    // Update is called once per frame
    void Update () { 
        
    } 
}
