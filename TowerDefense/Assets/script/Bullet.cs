using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
   private Enemy target;
   private float dmg;
   public float velocity = 90;

   public void SetBullet(Enemy target, float dmg){
    this.target = target;
    this.dmg = dmg;
   }

    // Update is called once per frame
    void Update()
    {
        if (target != null){
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, velocity * Time.deltaTime);
            float distance = Vector3.Distance(transform.position, target.transform.position);
            if (distance <= 0.1f){
                target.TakeDamage(dmg);
                Destroy(gameObject);
            }
        }
    }
}
