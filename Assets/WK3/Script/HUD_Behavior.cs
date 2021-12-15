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
    - public Boolean isFiring: Used to signal if the user is firing or not
    - public Text Ammo: Used to display text on the HUD showing the current amount of ammo the user has
    - public Text reserveAmmo: Used to display text on the HUD showing the current amount of reserve ammo the user has
    - public Text Powercell_no: Used to display text on the HUD showing the current amount of powercells the user has.
    - public AudioClip gunCock: Used to access the sound when the user reloads or picks up the rifle
    - private Sprite rifleSprite: Used to store the HUD sprite of the rifle
    - private Sprite pistolSprite: Used to store the HUD sprite of the pistol. This is used to indicate what weapon the user is using 
    - private Image icon: Used to display the above sprites
    - Public GameObject H_Text: Used to store the HUD text describing the current gun being used. This is active when the handgun is used
    - Public GameObject R_Text: Used to store the HUD text describing the current gun being used. This is active when the rifle is used
    - public GameObject pc: Contains the script shooter to allow us to access/use the no_cell value, this
        ensures that the user knows the number of powercells they have. 
    - private shooter powercell_amount: A variable of type shooter. This allows us to use the pc GameObject in methods to 
        show the number of powercells the user currently has. 
        
    - public GameObject ham: Used to interact with the GunController script to get ammo values for the handgun
    - public GameObject ram: Used to interact with the GunController script to get ammo values for the rifle
    - private GunController ammo_amount: Used to acquire the GunController script so we can get the ammo values for the handgun
    - private GunController r_ammo_amount: Used to acquire the GunController script so we can get the ammo values for the rifle

    - public int tempAmmo: used to store the Handguns ammo when the rifle is active
    - public int tempRAmmo: used to store the Handguns reserve ammo when the rifle is active

    - private Slider fuelBar: HUD device used to act as a fuel bar for the amount of fuel the user has in the jetpack
    - private JetpackBehavior jetpack: Used to access the jetpack script to get the current fuel amount/ratio/

**/ 

public class HUD_Behavior : MonoBehaviour
{
    public bool isFiring;
    public Text Ammo;
    public Text reserveAmmo ;
    public Text Powercell_no;
    public AudioClip gunCock;

    [SerializeField] private Sprite rifleSprite;
    [SerializeField] private Sprite pistolSprite;

    private Image icon;

    public GameObject H_Text;
    public GameObject R_Text;

    public GameObject pc;
    private shooter powercell_amount;

    public GameObject ham;
    public GameObject ram;
    private GunController ammo_amount;
    private GunController r_ammo_amount;
    public int tempAmmo;
    protected int tempRAmmo;

    private Slider fuelBar;
    private JetpackBehavior jetpack;

    // Start is called before the first frame update
    void Start()
    {
        powercell_amount = pc.GetComponent<shooter>();
        ammo_amount = ham.GetComponent<GunController>();
        icon = GameObject.Find("WeaponIcon").GetComponent<Image>();
        fuelBar = GameObject.Find("GOCanvas/HUD/FuelBar").GetComponent<Slider>();
        jetpack = GameObject.Find("FirstPerson-AIO").GetComponent<JetpackBehavior>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ammo_amount.isRifle == true){
            //if gun is rifle store the amount of ammo the handgun had
            tempAmmo = ammo_amount._currentAmmo;
            tempRAmmo = ammo_amount._currentReserve;

            icon.sprite = rifleSprite;
            R_Text.SetActive(true);
            H_Text.SetActive(false);

            r_ammo_amount = ram.GetComponent<GunController>();
            Ammo.text = r_ammo_amount._currentAmmo.ToString();
            reserveAmmo.text = r_ammo_amount._currentReserve.ToString();

            if(Input.GetMouseButtonDown(1) && !isFiring && r_ammo_amount._currentAmmo > 0){
            isFiring = true;
            isFiring = false;

            }
            // if ammo is equal to 0 then switch to the handgun with infinite ammo
            if((r_ammo_amount._currentAmmo <= 0) && (r_ammo_amount._currentReserve <= 0)){
                r_ammo_amount.isRifle = false;
                icon.sprite = pistolSprite;
                R_Text.SetActive(false);
                H_Text.SetActive(true);

                ham.SetActive(true);
                ram.SetActive(false);
                AudioSource.PlayClipAtPoint(gunCock, transform.position);

                // Get the temp ammo back for the handgun
                ammo_amount._currentAmmo = tempAmmo;
                ammo_amount._currentReserve = tempRAmmo;
                Ammo.text = ammo_amount._currentAmmo.ToString();
                reserveAmmo.text = ammo_amount._currentReserve.ToString();

            }

        }
        else{
            // Displays the ammo value to the HUD.
            Ammo.text = ammo_amount._currentAmmo.ToString();
            reserveAmmo.text = ammo_amount._currentReserve.ToString();
            if(Input.GetMouseButtonDown(1) && !isFiring && ammo_amount._currentAmmo > 0){
                isFiring = true;
                isFiring = false;
        }
        }
        
        //Display the number of powercells to the HUD.
        Powercell_no.text = powercell_amount.no_cell.ToString();

        #region JetPack Fuel

        //Get jetpack fuel ratio
        fuelBar.value = jetpack.getRatio();

        #endregion
        
    }
}
