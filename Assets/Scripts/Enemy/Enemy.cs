using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public static event Action<int, bool> OnPlayerHit;
    public static event Action<string> OnDeath;

    [SerializeField] private GameObject handHitObject;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Slider enemySlider;

    private Animator anim;
    private Transform player;

    public string UniqueID { get; set; }
    private int health = 0;

    private int damage = 25;

    private void Awake()
    {
        GenerateUniqueID();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        health = 100;
        enemySlider.value = health;

        player = GameObject.FindGameObjectWithTag("Player").transform;

        Sword.OnEnemyHit += HandlerGetAttack;
    }

    private void Update()
    {
        player.transform.LookAt(player);
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (UniqueID == id && health > 0)
        {
            health -= damage;
            enemySlider.value -= damage;
            //print($"Zombie with ID:{UniqueID}_" + health);
            anim.SetTrigger("isAttacked");
        }

        if (health <= 0)
        {
            enemySlider.gameObject.SetActive(false);
            anim.SetTrigger("isDead");
            OnDeath?.Invoke(UniqueID);
            Invoke("HandlerDeath", 1f);
        }
    }

    private void HandlerDeath()
    {
        Destroy(gameObject);
    }

    private void SwitchOnAttack()
    {
        handHitObject.SetActive(true);
    }

    private void SwitchOffAttack()
    {
        handHitObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            Transform playerTransform = other.transform;

            Vector3 directionToPlayer = (playerTransform.position - transform.position);

            float angle = Vector3.Angle(playerTransform.forward, -directionToPlayer);

            bool isFrontHit;
            int finalDamage = damage;

            if (angle > 120f)
            {
                finalDamage = Mathf.RoundToInt(damage * 1.5f);
                print("Hit back" + angle);
                isFrontHit = false;
            }
            else
            {
                isFrontHit = true;
                print("Hit front" + angle);
            }
            
            OnPlayerHit?.Invoke(finalDamage, isFrontHit);
        }
    }

    private void GenerateUniqueID()
    {
        UniqueID = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        Sword.OnEnemyHit -= HandlerGetAttack;
    }
}
