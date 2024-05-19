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

    [Header("OnDead")]
    public int moneyOnDead = 10;

    [Header("Damage")]
    public float dmg = 10;

    private SaludPlayer playerBase;
    private bool isAttacking = false;
    private WaveManager waveManager;  // Referencia al WaveManager
    
    private void Awake()
    {
        canvasRoot = FillLife.transform.parent.parent;
        initLifeRotation = canvasRoot.rotation;
        anim = GetComponent<Animator>();
        anim.SetBool("Movement", true);
        GetWayPoints();
        playerBase = FindObjectOfType<SaludPlayer>();
        waveManager = FindObjectOfType<WaveManager>();  // Buscar y asignar la referencia del WaveManager
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

    void Update()
    {
        canvasRoot.transform.rotation = initLifeRotation;
        Movement();
        LookAt();
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
                Debug.Log("Attack Base");
                StartCoroutine(AttackBase());
                return;
            }
            targetIndex++;
        }
    }

    private void LookAt()
    {
        var dir = waypoints[targetIndex].position - transform.position;
        var rootTarget = Quaternion.LookRotation(dir);
        transform.rotation = Quaternion.Slerp(transform.rotation, rootTarget, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator AttackBase()
    {
        if (playerBase != null && !isAttacking)
        {
            isAttacking = true;
            anim.SetTrigger("Attack");
            yield return new WaitForSeconds(GetAnimationLength("Attack01"));
            playerBase.TakeDmg(dmg);
            OnDead();
        }
    }

    private float GetAnimationLength(string animationName)
    {
        foreach (var clip in anim.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        return 0f;
    }

    public void TakeDamage(float dmg)
    {
        var newLife = currentLife - dmg;
        if (isDead) return;      
        if (newLife <= 0)
        {
            OnDead();
        }
        currentLife = newLife;
        var fillValue = currentLife / 100;
        FillLife.fillAmount = fillValue;
        currentLife = newLife;
    }

    private void OnDead()
    {
        isDead = true;
        anim.SetBool("Die", true);
        currentLife = 0;
        FillLife.fillAmount = 0;
        StartCoroutine(OnDeadEffect());
        PlayerData.instance.AddMoney(moneyOnDead);

        // Notificar al WaveManager que este enemigo ha sido derrotado
        if (waveManager != null)
        {
            waveManager.EnemyDefeated(this);
        }
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