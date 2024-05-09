using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    [Header("Movement")]
    public List<Transform> waypoints = new List<Transform>();
    private int targetIndex = 1;
    public float movementSpeed = 4;
    public float rotationSpeed = 6;
    private Animator anim;

    [Header("Life")]
    public bool isDead;
    public float maxLife = 100;
    public float currentLife = 0;
    public Image FillLife;
    private Transform canvasRoot;
    private Quaternion initLifeRotation;
    
    private void Awake()
    {
        canvasRoot = FillLife.transform.parent.parent;
        initLifeRotation = canvasRoot.rotation;
        anim = GetComponent<Animator>();
        anim.SetBool("Movement", true);
        GetWayPoints();
    }

    void Start()
    {
        currentLife = maxLife;
    }

    private void GetWayPoints()
    {
        waypoints.Clear();
        var rootWaypoints = GameObject.Find("WaypointsContainer").transform;
        for (int i = 0; i < rootWaypoints.childCount; i++)
        {
            waypoints.Add(rootWaypoints.GetChild(i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        canvasRoot.transform.rotation = initLifeRotation;
        Movement();
        LookAt();
        if(Input.GetKeyDown(KeyCode.Space))
        {
            TakeDamage(10);
        }
    }

private void Movement()
    {
        if (isDead) return;
        transform.position = Vector3.MoveTowards(transform.position, waypoints[targetIndex].position, movementSpeed * Time.deltaTime);
        var distance = Vector3.Distance(transform.position, waypoints[targetIndex].position);
        if (distance <= 0.1f)
        {
            if (targetIndex >= waypoints.Count - 1)
            {
                Debug.Log("LLEGASTE AL FINAL");
                return;
            }
            targetIndex++;
        }
    }
    private void LookAt()
    {
        //transform.LookAt(waypoints[targetIndex]);
        var dir = waypoints[targetIndex].position - transform.position;
        var rootTarget = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
    }

    public void TakeDamage(float dmg)
    {
        var newLife = currentLife - dmg;
        if(isDead) return;      
        if(newLife <= 0)
        {
            OnDead();
        }
        currentLife = newLife;
        var fillValue = currentLife / 100;
        FillLife.fillAmount = fillValue;
        currentLife = newLife;
        //StartCoroutine(AnimationDamage()); El duende no tiene animacion de recibir daño

    }
    /* private void AnimationDamage()  //El duende no tiene animacion de recibir daño
        {
            anim.SetBool("Damage", true);
            yield return new WaitForSeconds(0.1f);
            anim.SetBool("Damage", false);
        }*/
    private void OnDead()
    {
        isDead = true;
        anim.SetBool("Die", true);
        currentLife = 0;
        FillLife.fillAmount = 0;
        StartCoroutine(OnDeadEffect());
    }

    private IEnumerator OnDeadEffect()
    {
        yield return new WaitForSeconds(0.5f);
        var finalPositionY = transform.position.y - 5;
        Vector3 target = new Vector3(transform.position.x, finalPositionY, transform.position.z);
        while (transform.position.y != finalPositionY)
        {
            transform.position = Vector3.MoveTowards(transform.position, target, 1.5f * Time.deltaTime);
            yield return null;
        }
        Destroy(this);
    }
    
}
