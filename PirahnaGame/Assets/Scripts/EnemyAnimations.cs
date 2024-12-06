using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimations : MonoBehaviour
{
    Health healthScript;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        healthScript = GetComponent<Health>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DeathAnim()
    {
        anim.SetFloat("Health", 0);
        Wait(3);
    }

    private IEnumerator Wait(int seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
