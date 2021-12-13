using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HUD_Behavior : MonoBehaviour
{
    public int ammo = 5;
    public bool isFiring;
    public Text Ammo;
    public Text Powercell_no;

    public GameObject Handgun;
    public GameObject Rifle;

    public GameObject pc;
    private shooter powercell_amount;
    // Start is called before the first frame update
    void Start()
    {
        powercell_amount = pc.GetComponent<shooter>();
    }

    // Update is called once per frame
    void Update()
    {
        Ammo.text = ammo.ToString();
        if(Input.GetMouseButtonDown(1) && !isFiring && ammo > 0){
            isFiring = true;
            ammo --; 
            isFiring = false;

            if(ammo == 0){
                Rifle.active = !Rifle.active;
                Handgun.active = !Handgun.active;
            }

        }

        Powercell_no.text = powercell_amount.no_cell.ToString();


        
    }
}
