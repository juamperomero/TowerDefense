using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TowerUIPanelManager1 : MonoBehaviour
{
    private Tower tower;
    public TextMeshProUGUI towerNameTXT;
    public TextMeshProUGUI towerDescriptionTXT;
    public TextMeshProUGUI towerRangeTXT;
    public TextMeshProUGUI towerDMGTXT;
    public TextMeshProUGUI towerSpeedTXT;
    public TextMeshProUGUI towerSellPriceTXT;
    public TextMeshProUGUI towerUpgradePriceTXT;
    public GameObject root;
    public Button buttonUpgrade;
    public Button buttonSell;
    public static TowerUIPanelManager1 instance;
    private void Awake(){
        if(instance == null)    instance = this;
        else Destroy(this);
    }

    public void OpenPanel(Tower tower){
        if(tower == null){
            Debug.Log("Es necesario una torre.");
            return;
        }
        this.tower = tower;
        if(tower.currentIndexUpgrade >= tower.towerUpgradeData.Count){
            buttonUpgrade.gameObject.SetActive(false);
        }
        else{
            buttonUpgrade.onClick.AddListener(UpdateTower);
        }
        buttonSell.onClick.RemoveAllListeners();
        buttonSell.onClick.AddListener(SellTower);
        SetValues();
        root.SetActive(true);
    }

    public void UpdateTower(){
        if(tower == null){
            Debug.Log("Tower cannot be null."); 
            return;
        }
       
        if(PlayerData.instance.money >= tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice){
            tower.currentData = tower.towerUpgradeData[tower.currentIndexUpgrade];
            PlayerData.instance.TakeMoney(tower.towerUpgradeData[tower.currentIndexUpgrade].upgradePrice);
            if(tower.currentIndexUpgrade + 1 >= tower.towerUpgradeData.Count)   buttonUpgrade.gameObject.SetActive(false);
            else tower.currentIndexUpgrade++;
            OpenPanel(tower);
        }
        else{
            Debug.Log("Not have money.");
        }
    }

    public void SellTower(){
        if (tower != null){
            PlayerData.instance.AddMoney(tower.currentData.sellPrice);
            Destroy(tower.gameObject);
            ClosePanel();
        }
    }

    private void SetValues(){
        towerNameTXT.text = tower.towerName;
        towerDescriptionTXT.text = tower.towerDescription;
        towerRangeTXT.text = "Rango : " + tower.currentData.range +"";
        towerDMGTXT.text = "Daño : " + tower.currentData.dmg.ToString();
        towerSpeedTXT.text = "Velocidad : " + tower.currentData.timetoShoot.ToString();
        towerSellPriceTXT.text = "$ " + tower.currentData.sellPrice.ToString();
        towerUpgradePriceTXT.text = "$ " + tower.currentData.upgradePrice.ToString();
    }

    public void ClosePanel(){
        root.SetActive(false);
    }

}
