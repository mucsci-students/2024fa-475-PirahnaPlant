using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;  // Singleton instance
    public int totalMoney = 0;

    void Awake()
    {
        // Ensure that there is only one instance of MoneyScript
        if (Instance == null)
        {
            Instance = this;  // Set the instance to this script
            DontDestroyOnLoad(gameObject);  // Keep this object alive across scenes if needed
        }
        else
        {
            Destroy(gameObject);  // Destroy duplicate MoneyScript instances
        }
    }

    public void addToBalance(int update)
    {
        updateMoney(update);
    }

    public void updateMoney(int amount)
    {
        if(amount >= 0)
        {
            totalMoney += amount;
        }
        else if ((totalMoney + amount) < 0)
        {
            totalMoney = 0;
        }
    }

    public bool canBuy(int cost)
    {
        if (totalMoney < cost)
        {
            return false;
        }
        return true;
    }

    public void buyItem(int cost)
    {
        if (canBuy(cost))
        {
            updateMoney(-cost);
        }
    }

    public int getBalance()
    {
        return totalMoney;
    }
}