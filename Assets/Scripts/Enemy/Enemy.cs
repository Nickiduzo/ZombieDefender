using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour, IDamageable
{
    public static event Action<int, bool> OnPlayerHit;
    public static event Action<string, int> OnDeath;
    public static event Action<int> OnDamageUI;

    [SerializeField] private GameObject handHitObject;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private TextMeshProUGUI lvlText;

    [SerializeField] private GameObject loot;

    private Animator anim;

    private int currentHealth = 0;
    public event Action<int, int> OnHealthChanged;

    public string UniqueID { get; set; }
    public int maxHealth = 100;
    private int damage = 25;
    public int Lvl { get; set; }

    private void Awake()
    {
        GenerateEnemyData();
    }

    private void Start()
    {
        anim = GetComponent<Animator>();

        currentHealth = maxHealth;
        lvlText.text = Lvl.ToString();
        OnHealthChanged?.Invoke(currentHealth, maxHealth);

        Sword.OnEnemyHit += HandlerGetAttack;
    }

    public void TakeDamage(int damage)
    {
        Debug.Log("Enemy took damage " + damage);
        //TODO to transfer all logic from HandlerGetAttack and to test it
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (UniqueID == id && currentHealth > 0)
        {
            OnDamageUI?.Invoke(damage);
            currentHealth -= damage;
            //print($"Zombie with ID:{UniqueID}_" + health);
            anim.SetTrigger("isAttacked");
        }

        if (currentHealth <= 0)
        {
            anim.SetTrigger("isDead");
            StartCoroutine(DieWithDelay(1f));
        }

        OnHealthChanged?.Invoke(currentHealth, maxHealth);
    }

    private IEnumerator DieWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        OnDeath?.Invoke(UniqueID, Lvl);
        DropLoot();
        Destroy(gameObject);
    }

    private void DropLoot()
    {
        int isAvaible = UnityEngine.Random.Range(0, 100);
        if(isAvaible < 50)
        {
            Loot temp = Instantiate(loot, transform.position, Quaternion.identity).GetComponent<Loot>();
            if(temp != null)
            {
                temp.Initialization();
            }
            else
            {
                Debug.LogWarning("I can't determine enemie's loot");
            }
        }
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

    private void GenerateEnemyData()
    {
        UniqueID = Guid.NewGuid().ToString();

        Lvl = UnityEngine.Random.Range(PlayerManager.instance.Lvl, PlayerManager.instance.Lvl + 5);
        
        damage += Mathf.RoundToInt(damage * (Lvl / 100f));

        maxHealth += Mathf.RoundToInt(maxHealth * (Lvl / 100f));
    }

    private void OnDisable()
    {
        Sword.OnEnemyHit -= HandlerGetAttack;
    }
}
