﻿/// <summary>
/// WeaponSystem.cs
/// Author: MutantGopher
/// This script manages weapon switching.  It's recommended that you attach this to a parent GameObject of all your weapons, but this is not necessary.
/// This script allows the player to switch weapons in two ways, by pressing the numbers corresponding to each weapon, or by scrolling with the mouse.
/// </summary>

using UnityEngine;
using System.Collections;

public class WeaponSystem : MonoBehaviour
{
	public GameObject[] weapons;				// The array that holds all the weapons that the player has
	public int startingWeaponIndex = 0;			// The weapon index that the player will start with
	private int weaponIndex;					// The current index of the active weapon
	public AmmoUI ammoUI;
	public bool shotGunActive2 = false;
	public bool m4Active3 = false;
	public bool beamActive1 = false;
	public bool railGunActive4 = false;
	public bool pistol0 = true;

	// Use this for initialization
	void Start()
	{
		// Make sure the starting active weapon is the one selected by the user in startingWeaponIndex
		weaponIndex = startingWeaponIndex;
		SetActiveWeapon(weaponIndex);
		ammoUI.currentWeapon = weapons[weaponIndex].GetComponent<Weapon>();

        // Update the ammo UI
        ammoUI.UpdateAmmoUI();
	}
	
	// Update is called once per frame
	void Update()
	{
		// Allow the user to instantly switch to any weapon
		/*if (Input.GetButtonDown("Weapon 1"))
			SetActiveWeapon(0);
		if (Input.GetButtonDown("Weapon 2"))
			SetActiveWeapon(1);
		if (Input.GetButtonDown("Weapon 3"))
			SetActiveWeapon(2);
		if (Input.GetButtonDown("Weapon 4"))
			SetActiveWeapon(3);
		if (Input.GetButtonDown("Weapon 5"))
			SetActiveWeapon(4);
		if (Input.GetButtonDown("Weapon 6"))
			SetActiveWeapon(5);
		if (Input.GetButtonDown("Weapon 7"))
			SetActiveWeapon(6);
		if (Input.GetButtonDown("Weapon 8"))
			SetActiveWeapon(7);
		if (Input.GetButtonDown("Weapon 9"))
			SetActiveWeapon(8);*/

		// Allow the user to scroll through the weapons
		if (Input.GetAxis("Mouse ScrollWheel") > 0)
			NextWeapon();
		if (Input.GetAxis("Mouse ScrollWheel") < 0)
			PreviousWeapon();

			// Update the currentWeapon reference in AmmoUI
        ammoUI.currentWeapon = weapons[weaponIndex].GetComponent<Weapon>();

        // Update the ammo UI
        ammoUI.UpdateAmmoUI();
	}

	//activate weapon based on int passed
	public void activateWeapon(int activeNum){
		if(activeNum > 4){
			//do nothing
		}
		else if(activeNum == 4){
			railGunActive4 = true;
		}
		else if(activeNum == 3){
			m4Active3 = true;
		}
		else if(activeNum == 2){
			shotGunActive2 = true;
		}
		else if(activeNum == 1){
			beamActive1 = true;
		}
	}

	public bool isActive(int num){
		if(num > 4){
			return false;
		}
		else if(num == 4){
			return railGunActive4;
		}
		else if(num == 3){
			return m4Active3;
		}
		else if(num == 2){
			return shotGunActive2;
		}
		else if(num == 1){
			return beamActive1;
		}
		else if(num == 0){
			return pistol0;
		}
		return false;
	}

	void OnGUI()
	{


	}

	public void SetActiveWeapon(int index)
	{
		// Make sure this weapon exists before trying to switch to it
		if (index >= weapons.Length || index < 0)
		{
			Debug.LogWarning("Tried to switch to a weapon that does not exist.  Make sure you have all the correct weapons in your weapons array.");
			return;
		}

		// Send a messsage so that users can do other actions whenever this happens
		SendMessageUpwards("OnEasyWeaponsSwitch", SendMessageOptions.DontRequireReceiver);

		// Make sure the weaponIndex references the correct weapon
		if(index == 4 && !isActive(4)){
		weaponIndex = 0;
		return;
		}
		weaponIndex = index;

		// Make sure beam game objects aren't left over after weapon switching
		weapons[index].GetComponent<Weapon>().StopBeam();

		// Start be deactivating all weapons
		for (int i = 0; i < weapons.Length; i++)
		{
			weapons[i].SetActive(false);
		}

		// Activate the one weapon that we want
		weapons[index].SetActive(true);

		// Update the currentWeapon reference in AmmoUI
        ammoUI.currentWeapon = weapons[weaponIndex].GetComponent<Weapon>();

        // Update the ammo UI
        ammoUI.UpdateAmmoUI();
	}

	public void NextWeapon()
	{
		do{
			weaponIndex++;
		}
		while(isActive(weaponIndex) != true && !(weaponIndex > weapons.Length) && !(weaponIndex <= -1));
		//weaponIndex++;
		if (weaponIndex > weapons.Length - 1)
			weaponIndex = 0;
		SetActiveWeapon(weaponIndex);
		
	}

	public void PreviousWeapon()
	{
		do{
			weaponIndex--;
			}
		while(isActive(weaponIndex) != true && !(weaponIndex > weapons.Length - 1) && !(weaponIndex - 1 < 0));
		//weaponIndex--;
		if (weaponIndex < 0)
			weaponIndex = weapons.Length - 1;
		
		SetActiveWeapon(weaponIndex);
	}
}
