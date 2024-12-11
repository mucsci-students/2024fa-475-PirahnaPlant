using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamAmmo : MonoBehaviour
{
    public int ammoCapacity = 50;
    public int currentAmmo;
    public int fireRate = 1;
    public float timeFired = 0;
    public Weapon beamW;
    // Start is called before the first frame update
    void Start()
    {
        currentAmmo = ammoCapacity;
    }

    // Update is called once per frame
    void Update()
    {
        if(beamW.beaming){
            currentAmmo = currentAmmo - fireRate;
        }
        if(currentAmmo <= ammoCapacity){
           // beamW.beamPower = 0f;
        }
        reload();
    }

    public void reload(){
        currentAmmo = ammoCapacity;
    }
}
