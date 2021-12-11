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

    public Image muzzleFlashImage;
    public Sprite[] flashes;

    private void Start(){
        _currentAmmo = clipSize;
        _currentReserve = reservedAmmoCapacity;
        _canShoot = true;
    }

    private void update(){
        if(Input.getMouseButton(0) && _canShoot && _currentAmmo > 0){
            _canShoot = false;
            _currentAmmo--;
            StartCoroutine(ShootGun());
        }else if(Input.getKeyDown(KeyCode.R) && _currentAmmo < clipSize && _currentReserve > 0){
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
        StartCoroutine(muzzleFlash());
        yield return new WaitForSeconds(fireRate);
        _canShoot = true;
    }
    IEnumerator muzzleFlash(){
        muzzleFlashImage.sprite = flashes[Random.Range(0, flashes.length)];
        muzzleFlashImage.color = Color.white;
        yield return new WaitForSeconds(0.05f);
        muzzleFlashImage.sprite = null;
        muzzleFlashImage.color = new Color(0, 0, 0, 0);
    }
}
