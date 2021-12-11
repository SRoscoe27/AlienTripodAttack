using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour
{
    public GameObject powercell;

    public NavMeshAgent agent;
    public Transform player;
    public LayerMask whatIsGround, WhatIsPlayer;

    //Patrolling
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking
    public float attackInterval;
    bool alreadyAttacked;

    //states
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    //health
    public int health;

    public void Awake(){
        player = GameObject.Find ("FirstPerson-AIO").transform;
        agent = GetComponent<NavMeshAgent>();
        sightRange = 10f;
        attackRange = 1f;
        //defines sight and attack ranges
    }
    // Update is called once per frame
    void Update(){
        //this checks for sight range and attack range to change the state of the zombie
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, WhatIsPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, WhatIsPlayer);
        if(!playerInSightRange && !playerInAttackRange) Patrolling();
        if(playerInSightRange && !playerInAttackRange) ChasePlayer();
        if(playerInSightRange && playerInAttackRange) AttackPlayer();
        ChasePlayer();
    }
    public void SearchWalkPoint(){
        //this handles the walking points for random patrols
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);
        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround))
            walkPointSet = true;
    }
    public void Patrolling(){
        //handles walking around and patrolling
        if(!walkPointSet) SearchWalkPoint();
        if(walkPointSet)
            agent.SetDestination(walkPoint);
        Vector3 distanceToWalkPoint = transform.position - walkPoint;
        if(distanceToWalkPoint.magnitude < 1f)
            walkPointSet = false;
    }
    public void ChasePlayer(){
        //handles the navmesh chasing
        agent.SetDestination(player.position);
    }
    public void AttackPlayer(){
        //handles attack when player is close enough
        agent.SetDestination(transform.position);
        transform.LookAt(player);
        if(!alreadyAttacked){

            //insert the attack code here
            alreadyAttacked = true;
            Invoke("ResetAttack", attackInterval);
        }
    }
    public void ResetAttack(){
        alreadyAttacked = false;
        //this resets the attack for zombies
    }
    public void death(){
        //handles the death of the zombie and drops powercell, use after zombie is shot
        double chance = Random.Range(0f, 1f);
        if(chance > 0.6){
            GameObject temp = Instantiate(powercell, transform.position, Quaternion.identity) as GameObject;
            Destroy(gameObject);
        }
        else{
            Destroy(gameObject);
        }

        // insert code to change animation to death
        
        
    }
    void Hit(int damage){
        health -= damage;
        if(health<=0){
            death();
        }
    }
    void Start(){
        
    }
    
    
}
