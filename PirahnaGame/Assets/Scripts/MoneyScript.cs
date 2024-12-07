using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyScript : MonoBehaviour
{
    public int totalCoins = 0;
    //public GameObject player;
    
    void Start(){
        totalCoins = 0;
    gameObject.GetComponent<FirstPersonCharacter>().updateMoney(totalCoins);
    }

    public void addToBalance(int update){
        
        gameObject.GetComponent<FirstPersonCharacter>().updateMoney(update);
        totalCoins = gameObject.GetComponent<FirstPersonCharacter>().money;
        
        
    }

    public bool canBuy(int cost){
        if(gameObject.GetComponent<FirstPersonCharacter>().money < cost){
            return false;
        }
        return true;
    }

    public void buyItem(int cost){
        if(canBuy(cost)){
        addToBalance(-cost);
        }
    }

    public int getBalance(){
        return gameObject.GetComponent<FirstPersonCharacter>().money;
    }

}
