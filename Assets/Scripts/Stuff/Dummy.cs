using System;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public static event Action<int> OnDamage; 

    [SerializeField] private Animator anim;

    public string UniqueID { get; set; }

    private void Start()
    {
        GenerateUniqueID();

        Sword.OnHit += HandlerGetAttack;
    }

    private void HandlerGetAttack(string id, int damage)
    {
        if (UniqueID == id)
        {
            AudioManager.Instance.Play(AudioType.DummyHit);
            anim.SetTrigger("isAttacked");
            OnDamage(damage);
        }
    }

    private void GenerateUniqueID()
    {
        UniqueID = Guid.NewGuid().ToString();
    }

    private void OnDisable()
    {
        Sword.OnHit -= HandlerGetAttack;
    }
}
