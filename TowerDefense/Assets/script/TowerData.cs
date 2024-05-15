using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerData
{
    [Header("Price")]
    public int upgradePrice;
    public int buyPrice = 10;
    public int sellPrice = 8;

    [Header("Tower Settings")]
    public float range;
    public float dmg = 10;
    public float timetoShoot = 1;
}
