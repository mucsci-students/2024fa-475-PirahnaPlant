using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyUI : MonoBehaviour
{
    public TextMeshProUGUI moneyText;
    public FirstPersonCharacter player;

    void Start()
    {
        if (player.GetComponent<MoneyScript>().totalCoins >= 0){
        moneyText.text = "$: " + player.GetComponent<MoneyScript>().totalCoins;
        }
        else {
            moneyText.text = "$: 0";
        }
        if (moneyText == null)
        {
            Debug.LogError("No Ammo UI Text element assigned.");
        }
    }
    void Update(){
        UpdateMoneyUI();
    }
   public void UpdateMoneyUI()
    {
        if (player != null)
        {
            if (player.GetComponent<MoneyScript>().totalCoins >= 0){
        moneyText.text = "$: " + player.GetComponent<MoneyScript>().totalCoins;
        }
        else {
            moneyText.text = "$: 0";
        }
        }
    }
}
