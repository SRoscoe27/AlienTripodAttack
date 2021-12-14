using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
@author Sophia Roscoe
@version 3.0
The purpose of this script is to declare and control the behavior of the jetpack and fuel bar. This allows 
the user to escape the zombies and plan their next move of attack before they are overran. This is one of our 
significant features.

Variables include: 
    - private RigidBody rigid: Provides a rigidbody for us to add force to. This allows us to create the propulsion/physics 
        needed for the jetpack to function.
    - public GameObject Jetpack_Sound: GameObject to store the sound of the jetpack to be played when the jetpack is active.
        This provides an audio indication the jetpack is active.
    - public GameObject Jetpack_Smoke: GameObject to store the smoke particle effect for the jetpack, this provides a visual 
        indication that the jetpack is active.
    - public float fuel: Stores the total fuel the jetpack contains
    - public float fuelUsing: Stores the amount of fuel used, this is subtracted from fuel when the jetpack is used
    - public float gravity: Used to store a rough value of gravity. This is used during the physics to allow the jetpack to move up and down realistically
    - public float Jetforce: Used to calculate the velocity in which the user accelerates upwards.
    - public float maxSpeed: Used to limit the speed the user can accelerate to ensure they can't fly too high
    - public float distToGround: Value to store the distance the player is from the ground, used to see if the user
        is grounded. 
    - public bool isFlying: Used to indicate whether the user is flying
    - public Camera camera: Variable of type camera used to store the location of the camera.

    - Rect fuelRect: UI rectangles used to store the dimentions of the fuel bar at any given point
    - Texture2D fuelTexture: Displays the fuel bar on the UI Canvas
    - Vector2 pivotPoint: Used to rotate the fuel bar so that it is vertical on the left side of the screen
**/

public class JetpackBehavior : MonoBehaviour
{

    [SerializeField] 
    private Rigidbody rigid;
    public GameObject Jetpack_Sound; 
    public GameObject JetPackSmoke;

    public float fuel = 20f;
    public float fuelUsing = 4f;
    public float gravity = 10.0f;
    public float JetForce = 0.5f;
    public float maxSpeed = 6f;
    public float distToGround;
    public bool isFlying;
    public Camera camera;

    Rect fuelRect;
    Texture2D fuelTexture;
    Vector2 pivotPoint;

    // Start is called before the first frame update. This is used to declare various variables
    // and set up the fuel bar.
    void Start()
    {
        //Gets Rigidbody and Audiosource components and assigns their default position
        rigid = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        Jetpack_Sound.GetComponent<AudioSource>();
        Jetpack_Sound.SetActive(false);
    }

    // Public method returning a boolean value to indicate when the user is on the ground
    public bool isGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 1.1f);

    }

    public float getRatio(){
        return fuel/fuelUsing;
    }

    // Update is called once per frame
    void Update(){
        // if the amount of fuel is > 0 and the user presses the C button we start the Coroutine to start the jetpack functionality
        if((fuel >= 0) && (Input.GetKey(KeyCode.C))) StartCoroutine("Jetpack");
        //If the fuel runs out or user unpresses C then we stop the Coroutine and perform additional tasks. 
        if((fuel <= 0) && (Input.GetKeyUp(KeyCode.C))) {
            StopCoroutine("Jetpack");
            isFlying = false;

            // if user is no longer flying, stop the sound and smoke to indicate this.
            if (isFlying == false){
                JetPackSmoke.SetActive(false);
                Jetpack_Sound.SetActive(false);
            }
            
            //Add gravity force to bring user back down to the ground
            rigid.AddForce(new Vector3(0, gravity, 0));
            
            
            }
            
            // If the user is grounded and their fuel is less then 20, slowly add back the fuel.
            while(fuel<20f && isGrounded()){
                fuel += Time.deltaTime;
                Debug.Log(fuel);
            }
    }

    //Coroutine functionality for the Jetpack. 
    IEnumerator Jetpack (){
        //Set flying to true to indicate the user is flying and set the smoke and sound to active to indicate this
        isFlying = true;
        JetPackSmoke.SetActive(true);
        Jetpack_Sound.SetActive(true);
        Jetpack_Sound.transform.position = transform.position;
        
        //Define the velocity and the force applied to the rigidbody
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 forceToApply = JetForce * camera.transform.up;

        // If the users velocity is greater then the max speed then clamp it to make the user move at a constant speed. 
        if(rigid.velocity.magnitude > maxSpeed){
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);
        }
        //Add force to Rigidbody
        rigid.AddForce(forceToApply, ForceMode.VelocityChange);

        //Slowly decrement the fuel
        fuel -= fuelUsing * Time.deltaTime;
        Debug.Log(fuel);

        //Add negative gravity force to allow the user to move upwards and repeat until coroutine ends
        rigid.AddForce(new Vector3(0, -gravity, 0));
        yield return new WaitForSeconds(0.0f);

    }

}

