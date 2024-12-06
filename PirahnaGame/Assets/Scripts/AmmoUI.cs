using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AmmoUI : MonoBehaviour
{
    public TextMeshProUGUI ammoText;
    public Weapon currentWeapon;

    void Start()
    {
        if (currentWeapon == null)
        {
            Debug.LogError("No weapon assigned to AmmoUI.");
        }
        if (!(currentWeapon.isBeam())){
        ammoText.text = "Ammo: " + currentWeapon.ammoCapacity + " / " + currentWeapon.ammoCapacity;
        }
        else {
            ammoText.text = " ";
        }
        if (ammoText == null)
        {
            Debug.LogError("No Ammo UI Text element assigned.");
        }
    }

   public void UpdateAmmoUI()
    {
        if (currentWeapon != null)
        {
            if (!(currentWeapon.isBeam())){
            ammoText.text = "Ammo: " + currentWeapon.currentAmmo + " / " + currentWeapon.ammoCapacity;
            }
            else {
            ammoText.text = " ";
            }
        }
    }
}