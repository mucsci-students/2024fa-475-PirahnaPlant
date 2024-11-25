using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateHealthBar : MonoBehaviour
{
    public Transform cam;

    // Update is called once per frame
    void lateUpdate()
    {
        transform.LookAt(cam);
    }
}
