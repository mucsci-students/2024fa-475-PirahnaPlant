using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleTurretUI : MonoBehaviour
{
    private GameObject temp;
    public Canvas turretUI;
    private bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        temp = GameObject.Find("Turret_Menu");
        turretUI = temp.GetComponent<Canvas>();
        turretUI.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            isActive = !isActive;
            turretUI.enabled = isActive;
        }

        if (isActive)
        {
            Time.timeScale = 0f;
        }
        if (!isActive)
        {
            Time.timeScale = 1f;
        }
    }
}
