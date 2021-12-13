using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/**
@author Sophia Roscoe
@version 1.2
The purpose of this script is to control the behavior of assets on the HUD allowing it to interact with 
events in the game and not interfer with other UI menus. This creates a positive and immersive game experience 

Variables include: 
    - public int ammo: Contains the ammo amount for the rifle. Set to 5.
    - public Boolean isFiring: Used to signal if the user is firing or not
    - public Text Ammo: Used to display text on the HUD showing the current amount of ammo the user has
    - public Text Powercell_no: Used to display text on the HUD showing the current amount of powercells the user has.
    - public GameObject Handgun: Used to store the Handgun game object and its child compoants
        When active the handgun will appear on the HUD indicating that this is the users current weapon. 
    - public GameObject Rifle: Used to store the Rifle game object and its child compoants
        When active the rifle will appear on the HUD indicating that this is the users current weapon until the ammo has run out.
    - public GameObject pc: Contains the script shooter to allow us to access/use the no_cell value, this
        ensures that the user knows the number of powercells they have. 
    - public shooter powercell_amount: A variable of type shooter. This allows us to use the pc GameObject in methods to 
        show the number of powercells the user currently has. 

**/ 

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
        // Displays the ammo value to the HUD.
        Ammo.text = ammo.ToString();
        // if user uses the right click, the user is not currently firing and they have ammo perform the following
        if(Input.GetMouseButtonDown(1) && !isFiring && ammo > 0){
            isFiring = true;
            ammo --; 
            isFiring = false;

            // if ammo is equal to 0 then switch to the handgun with infinite ammo
            if(ammo == 0){
                Rifle.active = !Rifle.active;
                Handgun.active = !Handgun.active;
            }

        }

        //Display the number of powercells to the HUD.
        Powercell_no.text = powercell_amount.no_cell.ToString();


        
    }
}
