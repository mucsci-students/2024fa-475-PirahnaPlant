using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretPlacement : MonoBehaviour
{
    [SerializeField] GameObject[] turrets;
    [SerializeField] GameObject currentTurret;

    private Transform currentTransform;
    
    private int index = -1;

    private float rotationSpeed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
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
        for (int i = 0; i < turrets.Length; i++) {
            if (Input.GetKeyDown(KeyCode.Alpha0 + 1 + i))
            {
                if (SelectCurrentTurret(i))
                {
                    Destroy(currentTurret);
                    index = -1;
                } else
                {
                    if (currentTurret != null)
                    {
                        Destroy(currentTurret);
                    }
                    currentTurret = Instantiate(turrets[i]);
                    index = i;
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
        if (Input.GetKey(KeyCode.T))
        {
            if (index == 0)
            {
                currentTransform = currentTurret.transform;
                Destroy(currentTurret);
                currentTurret = Instantiate(turrets[4]);
                currentTurret.transform.position = currentTransform.position;
                currentTurret.transform.rotation = currentTransform.rotation;
                currentTurret = null;
            } else if (currentTurret == turrets[1])
            {
                currentTransform = currentTurret.transform;
                Destroy(currentTurret);
                currentTurret = Instantiate(turrets[5]);
                currentTurret.transform.position = currentTransform.position;
                currentTurret.transform.rotation = currentTransform.rotation;
                currentTurret = null;
            } else if (currentTurret == turrets[2]) {
                currentTransform = currentTurret.transform;
                Destroy(currentTurret);
                currentTurret = Instantiate(turrets[6]);
                currentTurret.transform.position = currentTransform.position;
                currentTurret.transform.rotation = currentTransform.rotation;
                currentTurret = null;
            } else if (currentTurret == turrets[3]) {
                currentTransform = currentTurret.transform;
                Destroy(currentTurret);
                currentTurret = Instantiate(turrets[7]);
                currentTurret.transform.position = currentTransform.position;
                currentTurret.transform.rotation = currentTransform.rotation;
                currentTurret = null;
            }


            // Original Code
            //currentTurret = null;

        }
    }
}
