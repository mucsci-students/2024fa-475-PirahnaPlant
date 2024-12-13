using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] GameObject[] turrets;
    [SerializeField] GameObject currentTurret;
    private int shotgunCost = 35;
    private int missileCost = 450;
    private int laserCost = 80;
    private int machineCost = 200;
    private int currCost = 0;
    private Transform currentTransform;
    
    private int index = -1;

    private float rotationSpeed = 200f;

    private Material turretMaterial;
    private Color originalColor;
    
    void Start()
    {
    }

    void Update()
    {
        CheckHotKey();

        if (currentTurret != null)
        {
            MoveCurrentTurret();
            Place();
        }
    }
    // Currently tied to ints num buttons, will change to UI butttons once implemented
    private void CheckHotKey()
    {
        for (int i = 0; i < turrets.Length / 2; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (SelectCurrentTurret(i))
                {
                    Destroy(currentTurret);
                    index = -1;
                }
                else
                {
                    if (currentTurret != null)
                    {
                        Destroy(currentTurret);
                    }
                    currentTurret = Instantiate(turrets[i]);
                    index = i;

                    //turretMaterial = currentTurret.GetComponent<Renderer>().material;
                    //originalColor = turretMaterial.color; 
                }
                break;
            }
        }
    }

    private bool SelectCurrentTurret(int i)
    {
        return index == i && currentTurret != null;
    }

    private void MoveCurrentTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
        if (Physics.Raycast(ray, out hitInfo))
        {
            currentTurret.transform.position = hitInfo.point;
            if (Input.GetKey(KeyCode.Q))
            {
                currentTurret.transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime, Space.World);
            }
            if (Input.GetKey(KeyCode.E))
            {
                currentTurret.transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime, Space.World);
            }
        }
    }

    private void Place()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            // Check if the player can afford the turret
            if (CanBuyTurret(index))
            {
                // Place the turret if the player has enough money
                MoneyScript.Instance.updateMoney(-currCost);
                if (index == 0)
                {
                    currentTransform = currentTurret.transform;
                    Destroy(currentTurret);
                    currentTurret = Instantiate(turrets[4]);
                    currentTurret.transform.position = currentTransform.position;
                    currentTurret.transform.rotation = currentTransform.rotation;
                    currentTurret = null;
                    //turretMaterial.color = originalColor;
                } 
                else if (index == 1)
                {
                    currentTransform = currentTurret.transform;
                    Destroy(currentTurret);
                    currentTurret = Instantiate(turrets[5]);
                    currentTurret.transform.position = currentTransform.position;
                    currentTurret.transform.rotation = currentTransform.rotation;
                    currentTurret = null;
                    //turretMaterial.color = originalColor;
                } 
                else if (index == 2) 
                {
                    currentTransform = currentTurret.transform;
                    Destroy(currentTurret);
                    currentTurret = Instantiate(turrets[6]);
                    currentTurret.transform.position = currentTransform.position;
                    currentTurret.transform.rotation = currentTransform.rotation;
                    currentTurret = null;
                    //turretMaterial.color = originalColor;
                } 
                else if (index == 3) 
                {
                    currentTransform = currentTurret.transform;
                    Destroy(currentTurret);
                    currentTurret = Instantiate(turrets[7]);
                    currentTurret.transform.position = currentTransform.position;
                    currentTurret.transform.rotation = currentTransform.rotation;
                    currentTurret = null;
                    //turretMaterial.color = originalColor;
                }
            }
            else
            {
                //turretMaterial.color = Color.red;
            }
        }
    }

    private bool CanBuyTurret(int turretIndex)
    {
        // Check the cost of the turret based on the turretIndex and see if the player has enough money
        if (turretIndex == 0 || turretIndex == 4) 
        {
            currCost = shotgunCost;
            return MoneyScript.Instance.canBuy(shotgunCost);
        }
        else if (turretIndex == 1 || turretIndex == 5) 
        {
            currCost = laserCost;
            return MoneyScript.Instance.canBuy(laserCost);
        }
        else if (turretIndex == 2 || turretIndex == 6) 
        {
            currCost = machineCost;
            return MoneyScript.Instance.canBuy(machineCost);
        }
        else if (turretIndex == 3 || turretIndex == 7) 
        {
            currCost = missileCost;
            return MoneyScript.Instance.canBuy(missileCost);
        }

        return false;
    }
}
