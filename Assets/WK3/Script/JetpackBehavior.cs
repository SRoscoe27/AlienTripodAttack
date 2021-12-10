using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public Camera camera;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        Jetpack_Sound.GetComponent<AudioSource>();
        Jetpack_Sound.SetActive(false);
    }

    public bool isGrounded(){
        return Physics.Raycast(transform.position, -Vector3.up, distToGround + 0.1f);

    }

    void Update(){
        if((fuel >= 0) && (Input.GetKey(KeyCode.C))) StartCoroutine("Jetpack");
        if((fuel <= 0) && (Input.GetKeyUp(KeyCode.C))) {
            StopCoroutine("Jetpack");

            if(fuel<=0 || (fuel > 0 && isGrounded())){
                JetPackSmoke.SetActive(false);
                Jetpack_Sound.SetActive(false);
            }
            
            rigid.AddForce(new Vector3(0, gravity, 0));

            while(fuel<20 && isGrounded()){
                fuel += fuelUsing * Time.deltaTime;
                Debug.Log(fuel);
            }
            
            }

    }

    IEnumerator Jetpack (){
        JetPackSmoke.SetActive(true);
        Jetpack_Sound.SetActive(true);
        Jetpack_Sound.transform.position = transform.position;
        
        Vector3 velocity = GetComponent<Rigidbody>().velocity;
        Vector3 forceToApply = JetForce * camera.transform.up;

        if(rigid.velocity.magnitude > maxSpeed){
            rigid.velocity = Vector3.ClampMagnitude(rigid.velocity, maxSpeed);
        }
        rigid.AddForce(forceToApply, ForceMode.VelocityChange);

        fuel -= fuelUsing * Time.deltaTime;
        Debug.Log(fuel);

        rigid.AddForce(new Vector3(0, -gravity, 0));
        yield return new WaitForSeconds(0.0f);

    }

}
