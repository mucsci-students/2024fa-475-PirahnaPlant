using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public Weapon currentWeapon;
    public BeamAmmo beamAmmo;

    void Start()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("No weapon assigned to AmmoUI.");
        }
        if(currentWeapon.GetComponent<BeamAmmo>() != null){
            beamAmmo = currentWeapon.GetComponent<BeamAmmo>();
            ammoText.text = "Ammo: " + currentWeapon.GetComponent<BeamAmmo>().ammoCapacity + " / " + currentWeapon.GetComponent<BeamAmmo>().ammoCapacity;
        }
        else 
        ammoText.text = "Ammo: " + currentWeapon.ammoCapacity + " / " + currentWeapon.ammoCapacity;
        
        if (ammoText == null)
        {
            Debug.LogError("No Ammo UI Text element assigned.");
        }
    }

   public void UpdateAmmoUI()
    {
        if (currentWeapon != null)
        {
            if(currentWeapon.GetComponent<BeamAmmo>() != null){
            beamAmmo = currentWeapon.GetComponent<BeamAmmo>();
            ammoText.text = "Ammo: " + currentWeapon.GetComponent<BeamAmmo>().currentAmmo + " / " + currentWeapon.GetComponent<BeamAmmo>().ammoCapacity;
        }
            ammoText.text = "Ammo: " + currentWeapon.currentAmmo + " / " + currentWeapon.ammoCapacity;
        }
    }
}