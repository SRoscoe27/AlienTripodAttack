using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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

    public void SwapToRifle(){
        isRifle = true;
        rifle.SetActive(true);
        pistol.SetActive(false);
    }

    private void Start(){

        _currentAmmo = clipSize;
        _currentReserve = reservedAmmoCapacity;
        _canShoot = true;
        rifleCheck = isRifles.GetComponent<shooter>();
    }

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

    IEnumerator ShootGun(){
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
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

