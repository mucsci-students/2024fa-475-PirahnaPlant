using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public static MoneyScript Instance;  
    public int totalMoney = 0;

    void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;  
            DontDestroyOnLoad(gameObject);  
        }
        else
        {
            Destroy(gameObject);  
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