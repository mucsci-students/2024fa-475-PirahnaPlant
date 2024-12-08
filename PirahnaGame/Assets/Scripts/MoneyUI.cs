using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public GameObject moneyManager;

    void Start()
    {
        if (moneyManager.GetComponent<MoneyScript>().totalMoney >= 0){
        moneyText.text = "$: " + moneyManager.GetComponent<MoneyScript>().totalMoney;
        }
        else {
            moneyText.text = "$: 0";
        }
        if (moneyText == null)
        {
            Debug.LogError("No Money UI Text element assigned.");
        }
    }
    void Update(){
        UpdateMoneyUI();
    }
   public void UpdateMoneyUI()
    {
        if (moneyManager != null)
        {
            if (moneyManager.GetComponent<MoneyScript>().totalMoney >= 0){
        moneyText.text = "$: " + moneyManager.GetComponent<MoneyScript>().totalMoney;
        }
        else {
            moneyText.text = "$: 0";
        }
        }
    }
}
