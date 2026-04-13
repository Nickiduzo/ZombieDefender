using System;
using UnityEngine;

public class CharacterAnimation : MonoBehaviour
{
    public static event Action<bool> OnBlock;
    [SerializeField] private GameObject leftSword;
    [SerializeField] private GameObject rightSword;

    [SerializeField] private float timer = 1f;
    private float time;

    private bool isDialogGoing = false;

    private Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
     
        CharacterMovement.OnMoveAnimation += HandlerMoveAnimation;
        CharacterMovement.OnJump += HandlerJumpAnimation;
        CharacterMovement.OnGround += HandlerLandAnimation;
    
        CharacterCombat.OnAttack += HandlerAttackAnimation;

        time = 0f;
    }

    private void Update()
    {
        if(DialogManager.Instance.GetCurrentDialogState())
        {
            isDialogGoing = true;
            DialogHandler();
            return;
        }
        else
        {
            isDialogGoing = false;
        }

        if (PlayerManager.instance.IsPlayerDead())
        {
            return;
        }

        if (time > 0f)
        {
            time -= Time.deltaTime;
        }

        if (time <= 0f && Input.GetKeyDown(KeyCode.Z) && !PlayerManager.instance.isNotInteractable && !PlayerManager.instance.isJumped)
        {
            PlayerManager.instance.PlayerRolling();
            anim.SetTrigger("isRoll");
            time = timer;
        }

        if (Input.GetMouseButton(2))
        {
            anim.SetBool("isBlock", true);
            OnBlock?.Invoke(true);
        }

        if (Input.GetMouseButtonUp(2))
        {
            anim.SetBool("isBlock", false);
            OnBlock?.Invoke(false);
        }
    }

    private void HandlerMoveAnimation(float x, float z)
    {
        if(x != 0 || z != 0)
        {
            anim.SetBool("isMove", true);
        }
        else
        {
            anim.SetBool("isMove", false);
        }

        anim.SetFloat("x", x);
        anim.SetFloat("z", z);

        if(Input.GetKey(KeyCode.W))
        {
            if (Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("z", 1.2f);
            }
        
            if(Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("x", -1.2f);
            }

            if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift))
            {
                anim.SetFloat("x", 1.2f);
            }
        }
    }

    private void HandlerJumpAnimation(bool isJump)
    {
        anim.SetBool("isJumping", isJump);
        PlayJumpSound();
    }

    private void HandlerLandAnimation(bool isGrounded)
    {
        anim.SetBool("isGrounded", isGrounded);
        
        if (isGrounded)
        {
            anim.SetBool("isJumping", false);
            anim.SetBool("isFalling", false);
        }
        else
        {
            anim.SetBool("isFalling", true);
        }
    }

    private void HandlerAttackAnimation(int currentAttack)
    {
        if (isDialogGoing) return;
        switch (currentAttack)
        {
            case 0: anim.SetTrigger("leftAttack");
                break;
            case 1: anim.SetTrigger("rightAttack");
                break;
            default:
                break;
        }
    }

    private void DialogHandler()
    {
        anim.SetBool("isMove", false);
        anim.SetBool("isJumping", false);
        anim.SetBool("isGrounded", true);
        anim.SetBool("isFalling", false);
        anim.SetBool("isBlock", false);
    }

    private void PlayJumpSound()
    {
        AudioManager.Instance.Play(AudioType.Jump);
    }

    public void PlayLandSound()
    {
        AudioType type = GetGroundType() == AudioType.GrassStep ? AudioType.GroundLand : AudioType.RockLand;
        AudioManager.Instance.Play(type);
    }

    public void PlayStep()
    {
        AudioType type = GetGroundType();
        AudioManager.Instance.Play(type);
    }

    // Future logic for detecting ground
    private AudioType GetGroundType()
    {
        RaycastHit hit;

        Vector3 origin = transform.position + Vector3.up * 0.1f; // Slightly above the character's position

        if (Physics.Raycast(origin, Vector3.down, out hit, 1.5f))
        {
            if (hit.collider.CompareTag("Ground"))
            {
                return AudioType.GrassStep;
            }
            else if (hit.collider.CompareTag("Rock"))
            {
                return AudioType.RockStep;
            }
        }

        return AudioType.GrassStep;
    }

    //

    #region AnimationTrigger

    #region Left

    private void SwitchOnLeftSword()
    {
        leftSword.SetActive(true);
    }

    private void SwitchOffLeftSword()
    {
        leftSword.SetActive(false);
    }

    #endregion

    #region Right

    private void SwitchOnRightSword()
    {
        rightSword.SetActive(true);
    }

    private void SwitchOffRightSword()
    {
        rightSword.SetActive(false);
    }

    #endregion

    #endregion


    private void OnDisable()
    {
        CharacterMovement.OnMoveAnimation -= HandlerMoveAnimation;
        CharacterMovement.OnJump -= HandlerJumpAnimation;
        CharacterMovement.OnGround -= HandlerLandAnimation;

        CharacterCombat.OnAttack -= HandlerAttackAnimation;
    }
}