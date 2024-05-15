using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData instance;
    public int money = 100;
    private void Awake(){
        #region Singleton
            if(instance == null) instance = this;
            else Destroy(gameObject);
        #endregion
    }

    private void Start(){
        PlayerDataUI.instance.UpdateMoneyText(money.ToString());
    }

    public void TakeMoney(int amount){
        money -= amount;
        if(money < 0)   money = 0;
        PlayerDataUI.instance.UpdateMoneyText(money.ToString());
    }
    
    public void AddMoney(int amount){
        money += amount;
        PlayerDataUI.instance.UpdateMoneyText(money.ToString());
    }

}
