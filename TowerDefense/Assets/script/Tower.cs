using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string towerDescription;
    public TowerData currentData;
    public Enemy currentTarget;
    public List<Enemy> currentTargets = new List<Enemy>();

    public Transform rotationPart;
    public Transform shootPosition;

    public GameObject shootEffect;
    public Bullet bullet;

    [Header("Tower Upgrade")]
    public List<TowerData> towerUpgradeData = new List<TowerData>();
    public int currentIndexUpgrade = 0;

    private void Start(){
        StartCoroutine(ShootTimer());
    }

    private void OnMouseDown(){
        TowerUIPanelManager.instance.OpenPanel(this);
    }

    private void Update(){
        EnemyDetection();
        LookRotation();
    }

    private void EnemyDetection(){
        currentTargets = Physics.OverlapSphere(transform.position, currentData.range).Where(currentEnemy => currentEnemy.GetComponent<Enemy>()).Select(currentEnemy => currentEnemy.GetComponent<Enemy>()).Where(currentEnemy => !currentEnemy.isDead).ToList();
        if(currentTargets.Count > 0 && !currentTargets.Contains(currentTarget)) currentTarget = currentTargets[0];
        else if(currentTargets.Count == 0) currentTarget = null;
    }

    private void LookRotation(){
        if(currentTarget) rotationPart.LookAt(currentTarget.transform);
    }

    private IEnumerator ShootTimer(){
        while(true){
            if(currentTarget){
                Shoot();
                shootEffect.SetActive(true);
                StartCoroutine(DesactiveShootEffect());
                yield return new WaitForSeconds(currentData.timetoShoot);
            } 
            yield return null;
        }
    }

    private IEnumerator DesactiveShootEffect(){
        yield return new WaitForSeconds(0.1f);
        shootEffect.SetActive(false);
    }


    private void Shoot(){
        var bulletGo = Instantiate(bullet, shootPosition.position, shootPosition.rotation);
        bulletGo.SetBullet(currentTarget, currentData.dmg);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, currentData.range);
    }

}
