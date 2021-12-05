using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public NavMeshAgent agent;
    public GameObject player;
    void Start(){
        player = GameObject.Find ("FirstPerson-AIO");
    }
    
    // Update is called once per frame
    void Update(){
        agent.SetDestination(player.transform.position); // position of player
    }
}
