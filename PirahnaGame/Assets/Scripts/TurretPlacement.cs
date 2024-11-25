using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    bool isPlayerInZone = false;
    [SerializeField] private GameObject turretUI;

    // Start is called before the first frame update
    void Start()
    {
        turretUI.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Regular_Character")
        {
            isPlayerInZone = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Regular_Character")
        {
            isPlayerInZone = false;
        }
    }

    IEnumerator watchForKeyPress()
    {
        while (isPlayerInZone)
        {
            if (Input.GetKey(KeyCode.E))
            {

            }
            yield return null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
