using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class TowerRequestManager : MonoBehaviour
{
    public List<Tower> towers = new List<Tower>();
    private Animator anim;
    public static TowerRequestManager instance;
    
    private void Awake(){
        if(!instance) instance = this; 
        else Destroy(instance);
        anim = GetComponent<Animator>();
    }

    public void OnOpenRequestPanel(){
         anim.SetBool("isOpen", true);
    }

    public void OnCloseRequestPanel(){
         anim.SetBool("isOpen", false);
    }

    public void RequestTowerBuy(string towerName){
        var tower = towers.Find(x => x.towerName == towerName);
        var towerGo = Instantiate(tower, Node.selectedNode.transform.position, tower.transform.rotation);
        OnCloseRequestPanel();
        Node.selectedNode.OnCloseSelection();
        Node.selectedNode = null;
    }
    

}
