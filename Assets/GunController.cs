using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GunController : MonoBehaviour{
    [Header("Gun Settings")]
    public float fireRate = 2.0f;
    public int clipSize = 10;
    public int reservedAmmoCapacity = 150;

    bool _canShoot;
    int _currentAmmo;
    int _currentReserve;

    private void Start(){
        _currentAmmo = clipSize;
        _currentReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    private void update(){
        if(Input.GetMouseButton(0) && _canShoot && _currentAmmo > 0){
            Debug.Log("Shooting gun");
            _canShoot = false;
            _currentAmmo--;
            StartCoroutine(ShootGun());
        }else if(Input.GetKeyDown(KeyCode.R) && _currentAmmo < clipSize && _currentReserve > 0){
            int amountNeeded = clipSize - _currentAmmo;
            if(amountNeeded>=_currentReserve){
                _currentAmmo += amountNeeded;
                _currentReserve -= amountNeeded;
            }else{
                _currentAmmo += _currentReserve;
                _currentReserve -= amountNeeded;
            }
        }
    }

    IEnumerator ShootGun(){
        RayCastForEnemy();
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
    void RayCastForEnemy(){
        RaycastHit hit;
        if(Physics.Raycast(transform.parent.position, transform.parent.forward, out hit, 1 << LayerMask.NameToLayer("Alien"))){
            try
            {
                Debug.Log("Hit Enemy");
                 hit.collider.SendMessage("Hit", 1);
            }
            catch (System.Exception)
            {
            }
            
        }
    }

}

