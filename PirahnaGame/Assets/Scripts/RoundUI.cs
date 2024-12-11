using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundUI : MonoBehaviour
{
    public TextMeshProUGUI roundText;
    GameObject RoundManager;
    RoundManager script;

    // Start is called before the first frame update
    void Start()
    {
        RoundManager = GameObject.Find("RoundManager");
        script = RoundManager.GetComponent<RoundManager>();
        roundText.text = "Round: 0";
    }

    // Update is called once per frame
    void Update()
    {
        if (RoundManager != null)
        {
            roundText.text = "Round: " + script.roundNumber.ToString();
        }
    }
}
