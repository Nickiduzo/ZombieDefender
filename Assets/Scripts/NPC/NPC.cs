using TMPro;
using UnityEngine;

public class NPC : MonoBehaviour, IInteractable
{
    [Header("Messages")]
    [SerializeField] protected NPCData data;
    [SerializeField] protected GameObject container;
    [SerializeField] protected GameObject questionPrefab;
    [SerializeField] protected GameObject textPrefab;

    [Header("Player Interaction")]
    [SerializeField] private Transform pointDefault;
    [SerializeField] private bool isInteraction = false;
    [SerializeField] private float distanceInteraction = 3f;
    private Transform player;


    private Animator anim;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null) Debug.Log("Can't find player transform");
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if(isInteraction && player != null)
        {
            LookAtTarget(player);
        }
        else
        {
            LookAtTarget(pointDefault);
        }

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);
        if(distanceToPlayer > distanceInteraction)
        {
            isInteraction = false;
        }
    }

    public virtual void Interact(Transform target)
    {
        anim.SetTrigger("isTalk");
        isInteraction = true;
        LookAtTarget(target);

        Debug.Log(name + " interaction");
    }

    private void LookAtTarget(Transform target)
    {  
        Vector3 direction = target.position - transform.position;
        direction.y = 0f;

        if (direction.magnitude > 0.1f)
        {
            Quaternion targetRotation = Quaternion.LookRotation(direction);
            transform.rotation = targetRotation;
        }
    }


    public string GetInteractText()
    {
        return name;
    }

    public Transform GetTransform()
    {
        return transform;
    }
}
