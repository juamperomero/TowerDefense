using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Tower : MonoBehaviour
{
    private Transform target;

    [Header("Attributes")]

    public string towerName;
    public string towerDescription;
    public float buyPrice;
    public float sellPrice;
    public float dmg = 20;
    public float range = 15f;
    public float fireRate = 1f;
    private float fireCountdown = 0f;

    [Header("Unity Setup Fields")]

    public string enemyTag = "enemy";
    public Transform partToRotate;
    public float turnSpeed = 10f;

    public GameObject bulletPrefab;
    public GameObject shootEffect;
    public Transform FirePoint;
    public Enemy currentTarget;
    private Enemy currentEnemyTarget;

    // Use this for initialization
    void Start () {
        InvokeRepeating ("UpdateTarget", 0f, 0.5f);
    }
    void UpdateTarget(){
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        currentTarget = null;
        foreach (GameObject enemyObj in enemies)
        {
            Enemy enemy = enemyObj.GetComponent<Enemy>();
            if (enemy != null)
            {
                float distanceToEnemy = Vector3.Distance(transform.position, enemyObj.transform.position);
                if (distanceToEnemy < shortestDistance && enemyObj != gameObject)
                {
                    shortestDistance = distanceToEnemy;
                    currentTarget = enemy;
                }
            }
        }
        if (currentTarget != null && shortestDistance <= range)
        {
            target = currentTarget.transform;
            currentEnemyTarget = currentTarget.GetComponent<Enemy>();
            //Debug.Log("Objetivo establecido en: " + target.name + " a una distancia de: " + shortestDistance);
        }
        else
        {
            target = null;
            //Debug.Log("No se encontró objetivo o está fuera de rango.");
        }
    }

    void Update () {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        if (dir.magnitude > 0f) {
            //Debug.Log("Calculando rotacion...");
            Quaternion lookRotation = Quaternion.LookRotation(dir);
            Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
            partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        }
        else {
            //Debug.Log("Salimos del calculo de la rotacion.");
        }

        if (fireCountdown <= 0f){
            Shoot();
            fireCountdown = 1f / fireRate;
        }
        fireCountdown -= Time.deltaTime;
    }

    void Shoot()
    {
        GameObject shootEffectInstance = Instantiate(shootEffect, FirePoint.position, FirePoint.rotation);
        ParticleSystem shootParticleSystem = shootEffectInstance.GetComponent<ParticleSystem>();
        float shootEffectDuration = shootParticleSystem.main.duration + shootParticleSystem.main.startLifetime.constant;
        Destroy(shootEffectInstance, shootEffectDuration);

        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, FirePoint.position, FirePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        currentEnemyTarget.TakeDamage(dmg);
        
        if (bullet != null) {
            bullet.Seek(target);
        }
    }


    private void OnDrawGizmosSelected(){
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, range);
    }

}
