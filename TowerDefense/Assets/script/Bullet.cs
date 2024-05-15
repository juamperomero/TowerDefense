using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

public class Bullet : MonoBehaviour
{
    private Enemy target;
    private float dmg;
    public float speed = 90;

    public void SetBullet(Enemy target, float dmg)
    {
        this.target = target;
        this.dmg = dmg;
    }

    void Update()
    {
        if (target != null)
        {
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f){
                target.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}

