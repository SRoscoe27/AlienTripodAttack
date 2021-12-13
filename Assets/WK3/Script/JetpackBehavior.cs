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
    public bool isFlying;
    public Camera camera;

    Rect fuelRect;
    Texture2D fuelTexture;
    Vector2 pivotPoint;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        distToGround = GetComponent<Collider>().bounds.extents.y;
        Jetpack_Sound.GetComponent<AudioSource>();
        Jetpack_Sound.SetActive(false);

        fuelRect = new Rect(Screen.width / 3, Screen.height / 10 - 200, Screen.width / 3, Screen.height / 30);
        fuelRect.y -= fuelRect.height;

        fuelTexture = new Texture2D(1,1);
        fuelTexture.SetPixel(0,0, Color.red);
        fuelTexture.Apply();
    }

    public bool isGrounded(){
        return Physics.Raycast(transform.position, Vector3.down, distToGround + 1.1f);

    }

    void OnGUI(){
        pivotPoint = new Vector2(Screen.width / 2, Screen.height / 2);
        GUIUtility.RotateAroundPivot(270, pivotPoint);

        float ratio = fuel/fuelUsing;
        float rectWidth = ratio * Screen.width / 3;
        fuelRect.width = rectWidth;
        GUI.DrawTexture(fuelRect, fuelTexture);

    }

    void Update(){
        if((fuel >= 0) && (Input.GetKey(KeyCode.C))) StartCoroutine("Jetpack");
        if((fuel <= 0) && (Input.GetKeyUp(KeyCode.C))) {
            StopCoroutine("Jetpack");
            isFlying = false;

            if (isFlying == false){
                JetPackSmoke.SetActive(false);
                Jetpack_Sound.SetActive(false);
            }
            
            rigid.AddForce(new Vector3(0, gravity, 0));
            
            while(fuel<20f && isGrounded()){
                fuel += Time.deltaTime;
                Debug.Log(fuel);
            }
            
            }

    }

    IEnumerator Jetpack (){
        isFlying = true;
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

