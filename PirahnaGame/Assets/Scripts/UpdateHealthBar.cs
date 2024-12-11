using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    [SerializeField] private Canvas canvas;
    GameObject character;
    Transform cam;

    // Find charcater's transform to point health bars at
    private void Start()
    {
        character = GameObject.Find("Regular_Character");
        cam = character.transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 targetPosition = cam.transform.position;
        canvas.transform.LookAt(targetPosition, Vector3.up);
    }
}
