using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager instance;

    private bool isPlayerDead = false;

    [Header("Combat and moving")]
    public float attackTimer = 0f;
    public float hitTimer = 0f;
    public float rollTimer = 0f;

    public bool isNotInteractable = false;

    private float currentTimer = 0f;

    [Header("Jumping")]
    public float jumpTimer = 0f;
    public bool isJumped = false;

    private float currentJumpTimer = 0f;

    public int Lvl { get; set; }

    [Header("UI Cooldowns")]
    [SerializeField] private Image leftAttackImage;
    [SerializeField] private Image rightAttackImage;
    [SerializeField] private Image rollCooldownImage;
    [SerializeField] private Image jumpCooldownImage;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        CharacterHealth.OnPlayerDeath += GetCurrentPlayerHealth;

        leftAttackImage.fillAmount = 1f;
        rightAttackImage.fillAmount = 1f;
        rollCooldownImage.fillAmount = 1f;
        jumpCooldownImage.fillAmount = 1f;
    }

    private void Update()
    {
        if (isNotInteractable)
        {
            if (currentTimer <= 0f)
            {
                isNotInteractable = false;
            }
            else
            {
                currentTimer -= Time.deltaTime;
            }
        }

        if (isJumped)
        {
            currentJumpTimer -= Time.deltaTime;
            
            if (currentJumpTimer <= 0f)
            {
                isJumped = false;
            }
        }

        #region UICooldowns

        if (leftAttackImage.fillAmount < 1f)
        {
            leftAttackImage.fillAmount += Time.deltaTime / attackTimer;
        }

        if (rightAttackImage.fillAmount < 1f)
        {
            rightAttackImage.fillAmount += Time.deltaTime / attackTimer;
        }

        if (jumpCooldownImage.fillAmount < 1f)
        {
            jumpCooldownImage.fillAmount += Time.deltaTime / jumpTimer;
        }

        if (rollCooldownImage.fillAmount < 1f)
        {
            rollCooldownImage.fillAmount += Time.deltaTime / rollTimer;
        }
        
        #endregion
    }

    private void GetCurrentPlayerHealth(bool state)
    {
        isPlayerDead = state;
    }

    public void PlayerRolling()
    {
        isNotInteractable = true;
        currentTimer = rollTimer;

        rollCooldownImage.fillAmount = 0f;
    }

    public void SetAttack(int type)
    {
        isNotInteractable = true;
        currentTimer = attackTimer;

        if (type == 1)
        {
            leftAttackImage.fillAmount = 0f;
        }
        else
        {
            rightAttackImage.fillAmount = 0f;
        }
    }

    public void SetJump()
    {
        isJumped = true;
        currentJumpTimer = jumpTimer;

        jumpCooldownImage.fillAmount = 0f;
    }
    
    public void GetHit()
    {
        isNotInteractable = true;
        currentTimer = hitTimer;
    }

    public bool IsPlayerDead()
    {
        return isPlayerDead;
    }
    private void OnDisable()
    {
        CharacterHealth.OnPlayerDeath -= GetCurrentPlayerHealth;
    }
}
