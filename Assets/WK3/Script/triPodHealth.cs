using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class triPodHealth : MonoBehaviour
{
    private float health = 3; 
    public GameObject smoke, flare; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    //Decrease health when tripod is hit. 
    public void reduceHealth(){ 
        health --; 
    }

    //For eahch collision, check if health is 0 and activate appropreate GameObjects if so. 
    void OnCollisionEnter(Collision other){
        if (health <= 0){ 
            smoke.SetActive(true); 
            flare.SetActive(true); 
        } 
    }

    // Update is called once per frame
    void Update () { 
        
    } 
}
