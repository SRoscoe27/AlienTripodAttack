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

    public GameObject pistol;
    public GameObject rifle;

    bool _canShoot;
    public int _currentAmmo;
    public int _currentReserve;

    private void Start(){

        _currentAmmo = clipSize;
        _currentReserve = reservedAmmoCapacity;
        _canShoot = true;
        
        isRifle = false;
    }

    private void Update(){
        if(Input.GetMouseButton(1) && _canShoot && _currentAmmo > 0){
            Debug.Log("Shooting gun");
            _canShoot = false;
            _currentAmmo--;
            StartCoroutine(ShootGun());
        }else if(Input.GetKeyDown(KeyCode.R) && _currentAmmo < clipSize && _currentReserve > 0){
            Debug.Log("reloading");
            int amountNeeded = clipSize - _currentAmmo;
            if(amountNeeded <= _currentReserve){
                _currentAmmo += amountNeeded;
                _currentReserve -= amountNeeded;
                Debug.Log("whyy");
            }else{
                _currentAmmo += _currentReserve;
                _currentReserve -= amountNeeded;
                Debug.Log("bhyy");
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
        Debug.Log("Raycast for enemy");
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit)){
            if(hit.transform.CompareTag("Alien")){
                float distance =  Vector3.Distance(hit.transform.position, transform.position);
                if(distance < range){
                    Debug.Log("Hit Enemy");
                    try{ 
                        hit.collider.SendMessage("Hit", 1);
                    }
                        catch (System.Exception){
                    }
                }else{
                    Debug.Log("Out of range");
                }
            }  
        }
    }
    public void SwapToRifle(){
        isRifle = true;
        rifle.SetActive(true);
        pistol.SetActive(false);
    }
}

