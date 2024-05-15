using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Node : MonoBehaviour
{
    public static Node selectedNode;
    private Animator anim;
    private bool IsSelected = false;
    public bool isOcuped;
    public Tower towerOcuped;
    
    private void Awake(){
        anim = GetComponent<Animator>();
    }

    private void OnMouseDown(){
        if(isOcuped){
            TowerUIPanelManager.instance.OpenPanel(towerOcuped);
            return;
        }
        if(selectedNode && selectedNode != this) selectedNode.OnCloseSelection();
        selectedNode = this;
        IsSelected = !IsSelected;
        if(IsSelected) TowerRequestManager.instance.OnOpenRequestPanel();
        else TowerRequestManager.instance.OnCloseRequestPanel();
        anim.SetBool("IsSelected", IsSelected);
    }

    public void OnCloseSelection(){
        IsSelected = false;
        anim.SetBool("IsSelected", IsSelected);
    }

}
