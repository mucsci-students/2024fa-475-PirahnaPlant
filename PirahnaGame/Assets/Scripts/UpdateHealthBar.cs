using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdateHealthBar : MonoBehaviour
{
    [SerializeField] private Slider slider;
    public Transform cam;

    public void UpdateHealthValue(float curHealth, float maxHealth)
    {
        slider.value = curHealth / maxHealth;
    }
    // Update is called once per frame
    void lateUpdate()
    {
        transform.LookAt(cam);
    }
}
