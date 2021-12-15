using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/** 
The purpose of this script is to define the behavior of the guns with the enemy zombies.
This script is used for both sets of guns with certain parameters such as range and damage being changed depending of the weapon the user is using

Variables include: 
    - public float fireRate: The speed at which the user can fire in seconds, this is longer for the handgun compared to the rifle
    - public int clipSize: The amount of bullets in the clip and how much they can shoot before they have to reload, this is higher for the rifle then the handgun
    - public int reservedAmmoCapacity: The amount of bullets a gun is allowed to have in reserve, this is larger for the handgun then for the rifle.
    - public float range: The range at which the bullets can hit, this is larger for the rifle then for the handgun
    - public bool isRifle: A local boolean to indicate if the user is holding the rifle, this changes the damage, ammo and range values
    - public GameObject isRifles: Game object to store the isRifle value from shooter
    - public shooter rifleCheck: stores the shooter script to allow us to check the value of isRifle in shooter

    - public GameObject pistol: used to store the Handgun prefab and used to change some parameter values
    - public GameObject rifle: used to store the Rifle prefab and used to change some parameter values
    - public GameObject bloodEffect: Used to store the blood prefab for when the zombie gets hit 
    - public float removeTime: The amount of time we want the bloodEffect to last for
    - public AudioClip shotSound: Used to access the sound when the user shoots the gun
    - public AudioClip gunCock: Used to access the sound when the user reloads or picks up the rifle
    - bool _canShoot: Boolean to indicate when a user can shoot
    - public int _currentAmmo: The current ammo the user currently has in their clip
    - public int _currentReserve: The current amount the user currently has in their reserve.
**/

public class GunController : MonoBehaviour{
    [Header("Gun Settings")]
    public float fireRate;
    public int clipSize;
    public int reservedAmmoCapacity;
    public float range;
    public bool isRifle;
    public GameObject isRifles;
    private shooter rifleCheck; 

    public GameObject pistol;
    public GameObject rifle;

    public GameObject bloodEffect;
    public float removeTime = 2.0f;
    public AudioClip shotSound;
    public AudioClip gunCock;

    bool _canShoot;
    public int _currentAmmo;
    public int _currentReserve;

    // Function to swap to rifle asset on the screen
    public void SwapToRifle(){
        isRifle = true;
        rifle.SetActive(true);
        pistol.SetActive(false);
    }

    // Start is called before the first frame update
    private void Start(){
        _currentAmmo = clipSize; //set up current ammo to be equal to the clip size and same for the reserve
        _currentReserve = reservedAmmoCapacity;
        _canShoot = true; // allow to shoot
        rifleCheck = isRifles.GetComponent<shooter>();
    }

    // Update is called once per frame, if user clicks the right click then play the ShootGun coroutine and edit the ammo values
    private void Update(){
        if(Input.GetMouseButton(1) && _canShoot && _currentAmmo > 0){
            Debug.Log("Shooting gun");
            AudioSource.PlayClipAtPoint(shotSound, transform.position);
            _canShoot = false;
            _currentAmmo--;
            StartCoroutine(ShootGun());
        }else if(Input.GetKeyDown(KeyCode.R) && _currentAmmo < clipSize && _currentReserve > 0){
            Debug.Log("reloading");
            AudioSource.PlayClipAtPoint(gunCock, transform.position);
            int amountNeeded = clipSize - _currentAmmo;
            if(amountNeeded <= _currentReserve){
                _currentAmmo += amountNeeded;
                _currentReserve -= amountNeeded;
            }else{
                _currentAmmo += _currentReserve;
                _currentReserve -= amountNeeded;
                if(isRifle){
                    rifle.SetActive(false);
                    pistol.SetActive(true);
                }
            }
        }
    }

    // IEnumerator function activated when the user wishes to shoot the gun, it allow the user to shoot and performs the RayCastForEnemy function
    IEnumerator ShootGun(){
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }

    // Function that when called checks to see if a zombie is in range and if so damage them
    void RayCastForEnemy(){
        Debug.Log("Raycast for enemy ");
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit)){
            if(hit.transform.CompareTag("Alien")){
                float distance =  Vector3.Distance(hit.transform.position, transform.position);
                if(distance < range){
                    Debug.Log("Hit Enemy");
                    GameObject blood = Instantiate(bloodEffect, hit.transform.position + new Vector3(0,1,0), Quaternion.identity) as GameObject;
                    Destroy(blood ,removeTime);
                    try{ 
                        if(rifleCheck.isRifle){
                             hit.collider.SendMessage("Hit", 3);
                        }else{
                             hit.collider.SendMessage("Hit", 1);
                        }
                    }
                        catch (System.Exception){
                    }
                }else{
                    Debug.Log("Out of range");
                }
            }  
        }
    }
}

