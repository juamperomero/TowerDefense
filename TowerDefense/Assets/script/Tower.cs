using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public string towerName;
    public string towerDescription;
    public float buyPrice;
    public float sellPrice;

    public float range;
    public float dmg = 20;
    public float timetoShot = 1;
    public Enemy currentTarget;
    public List<Enemy> currentTargets = new List<Enemy>();
    public Transform rotationPart;
    public Transform shootPosition;
    public GameObject shootEffect;
    public Bullet bullet;

    private void Start(){
        StartCoroutine(ShootTimer());
    }

    private void Update(){
        EnemyDetection();
        LookRotation();
    }

    private void EnemyDetection(){
        currentTargets = Physics.OverlapSphere(transform.position, range).Where(currentEnemy => currentEnemy.GetComponent<Enemy>()).Select(currentEnemy => currentEnemy.GetComponent<Enemy>()).Where(currentEnemy => !currentEnemy.isDead).ToList();
        if(currentTargets.Count > 0 && !currentTargets.Contains(currentTarget)) 
            currentTarget = currentTargets[0];
        else if(currentTargets.Count == 0) 
            currentTarget = null;
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
                yield return new WaitForSeconds(timetoShot);
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
        bulletGo.SetBullet(currentTarget, dmg);
    }

    private void OnDrawGizmos(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
