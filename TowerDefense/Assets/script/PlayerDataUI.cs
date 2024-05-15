using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerDataUI : MonoBehaviour
{
    public TextMeshProUGUI moneyTXT;
    public static PlayerDataUI instance;
    private void Awake(){
        #region Singleton
            if(instance == null) instance = this;
            else Destroy(gameObject);
        #endregion
    }

    public void UpdateMoneyText(string value){
        moneyTXT.text = "$ " + value;
    }
}
