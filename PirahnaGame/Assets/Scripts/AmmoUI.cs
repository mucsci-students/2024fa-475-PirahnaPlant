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
            ammoText.text = "Ammo: " + currentWeapon.currentAmmo + " / " + currentWeapon.ammoCapacity;
        }
    }
}